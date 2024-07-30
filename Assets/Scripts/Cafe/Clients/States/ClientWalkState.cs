using UnityEngine;

public class ClientWalkState : ClientBaseState {
    private Vector2 _nowTarget;
    private const float _speed = 3f;

    public override void EnterState(Client client) {
        bool isLeaving = client.Data.State == ClientState.Leave;
        if (isLeaving)
            _nowTarget = client.Spawner.SpawnPoint;
        else
            _nowTarget = client.Spawner.GetSpot(client.SpotIndex).GetTarget(client.TableIndex);
        _nowTarget = new Vector2(_nowTarget.x, client.transform.position.y);
        client.StartWalk(isLeaving.ToDirection());
    }

    public override void ExitState(Client client) {
        if (client.Data.State == ClientState.Leave)
            return;

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
                client.Wait();
        }
    }
}
