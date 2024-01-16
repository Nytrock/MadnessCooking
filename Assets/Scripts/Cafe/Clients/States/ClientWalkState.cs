using UnityEngine;

public class ClientWalkState : ClientState
{
    private Transform _target;
    private float _speed = 8f;

    public override void EnterState(Client client)
    {
        _client = client;
        _client.ChangeSortingGroup(10);
        if (_client.IsLeaving)
            _target = client.ExitTarget;
        else
            _target = client.EnterTarget;
        _client.RotateSkin(_client.IsLeaving);
    }

    public override void ExitState()
    {
        _client.RotateSkin();
        _client.ChangeSortingGroup(5);
    }

    public override void UpdateState()
    {
        _client.transform.position = Vector2.MoveTowards(_client.transform.position, _target.position, _speed * Time.deltaTime);
        if (Vector3.Distance(_client.transform.position, _target.position) < 0.001f) {
            if (_client.IsLeaving)
                _client.Destroy();
            else
                _client.Wait();
        }
    }
}
