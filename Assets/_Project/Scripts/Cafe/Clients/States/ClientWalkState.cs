using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class ClientWalkState : ClientBaseState {
        private Vector2 _nowTarget;
        private const float _speed = 3.75f;
        private ClientsSpawner _spawner;

        public void SetupSpawner(ClientsSpawner spawner) {
            _spawner = spawner;
        }

        public override void EnterState(Client client) {
            bool isLeaving = client.Data.State == ClientState.Leave;
            if (isLeaving)
                _nowTarget = _spawner.SpawnPoint;
            else
                _nowTarget = client.Seat.transform.position;
            _nowTarget = new Vector2(_nowTarget.x, client.transform.position.y);
            client.StartWalk(isLeaving.ToDirection());
        }

        public override void ExitState(Client client) {
            client.TakeSeat();
        }

        public override void UpdateState(Client client) {
            Transform clientPos = client.transform;

            clientPos.position = Vector2.MoveTowards(clientPos.position, _nowTarget, _speed * InGameTime.Instance.NormalizedDeltaTime);
            client.Data.UpdatePosition(clientPos.position);

            if (Mathf.Abs(clientPos.position.x - _nowTarget.x) < 0.1f) {
                if (client.Data.State == ClientState.Leave)
                    client.Destroy();
                else
                    client.SitAndWait();
            }
        }
    }
}
