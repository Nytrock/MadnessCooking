using UnityEngine;

public class ClientWalkState : ClientBaseState
{
    private Transform _target;
    private readonly float _speed = 8f;

    public override void EnterState(Client client)
    {
        bool isLeaving = client.ClientData.State == ClientState.Leave;
        if (isLeaving)
            _target = client.Spawner.SpawnPoint;
        else
            _target = client.Spawner.GetSpot(client.SpotIndex).GetTarget(client.TableIndex);
        client.RotateSkin(isLeaving.ToDirection());
    }

    public override void ExitState(Client client)
    {
        if (client.ClientData.State == ClientState.Leave)
            return;

        client.TakeSeat();
    }

    public override void UpdateState(Client client)
    {
        client.transform.position = Vector2.MoveTowards(client.transform.position, 
            _target.position, _speed * InGameTime.Instance.DeltaTime);
        client.ClientData.Position = new SerializableVector(client.transform.position);

        if (Vector2.Distance(client.transform.position, _target.position) < 0.001f) {
            if (client.ClientData.State == ClientState.Leave)
                client.Destroy();
            else
                client.Wait();
        }
    }
}
