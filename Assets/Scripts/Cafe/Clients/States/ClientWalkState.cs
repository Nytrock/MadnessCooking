using UnityEngine;

public class ClientWalkState : ClientBaseState {
    private float _target;
    private const float _speed = 3f;
    private float _directionMultiplier;

    public override void EnterState(Client client) {
        bool isLeaving = client.ClientData.State == ClientState.Leave;
        if (isLeaving)
            _target = client.Spawner.SpawnPoint.x;
        else
            _target = client.Spawner.GetSpot(client.SpotIndex).GetTarget(client.TableIndex).x;

        _directionMultiplier = isLeaving ? 1 : -1;
        client.StartWalk(isLeaving.ToDirection());
    }

    public override void ExitState(Client client) {
        if (client.ClientData.State == ClientState.Leave)
            return;

        client.TakeSeat();
    }

    public override void UpdateState(Client client) {
        client.transform.position += new Vector3(_speed * InGameTime.Instance.DeltaTime * _directionMultiplier, 0, 0);
        client.ClientData.UpdatePosition(client.transform.position);

        if (Mathf.Abs(client.transform.position.x - _target) < 0.1f) {
            if (client.ClientData.State == ClientState.Leave)
                client.Destroy();
            else
                client.Wait();
        }
    }
}
