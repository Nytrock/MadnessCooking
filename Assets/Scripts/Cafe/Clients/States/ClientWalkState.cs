using UnityEngine;

public class ClientWalkState : ClientState
{
    private Transform _target;
    private float _speed = 8f;

    public override void EnterState(Client client)
    {
        client.ChangeSortingGroup(10);
        if (client.IsLeaving)
            _target = client.ExitTarget;
        else
            _target = client.EnterTarget;
        client.RotateSkin(client.IsLeaving);
    }

    public override void ExitState(Client client)
    {
        client.RotateSkin();
        client.ChangeSortingGroup(5);
    }

    public override void UpdateState(Client client)
    {
        client.transform.position = Vector2.MoveTowards(client.transform.position, 
            _target.position, _speed * Time.deltaTime * TimeManager.instance.TimeSpeed);
        if (Vector2.Distance(client.transform.position, _target.position) < 0.001f) {
            if (client.IsLeaving) {
                client.Destroy();
            } else {
                if (client.InGroup())
                    client.SitWithGroup();
                else
                    client.Wait();
            }
        }
    }
}
