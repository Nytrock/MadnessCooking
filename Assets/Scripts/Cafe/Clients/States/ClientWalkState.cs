using UnityEngine;

public class ClientWalkState : ClientState
{
    private Client _client;
    private Transform _target;
    private float _speed = 8f;

    public override void EnterState(Client client)
    {
        _client = client;
        _client.SortingGroup.sortingOrder = 10;
        if (client.IsLeaving) {
            _target = client.ExitTarget;
        } else {
            _target = client.EnterTarget;
        }
    }

    public override void ExitState()
    {
        _client.transform.SetParent(_target);
        _client.SortingGroup.sortingOrder = 5;
    }

    public override void UpdateState()
    {
        _client.transform.position = Vector2.MoveTowards(_client.transform.position, _target.position, _speed * Time.deltaTime);
        if (Vector3.Distance(_client.transform.position, _target.position) < 0.001f) {
            _client.NowState = _client._waitState;
        }
    }
}
