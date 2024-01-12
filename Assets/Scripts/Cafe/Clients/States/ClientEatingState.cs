using UnityEngine;

public class ClientEatingState : ClientState
{
    private float _eatTime;
    private float _nowTime = 0;


    public override void EnterState(Client client)
    {
        _client = client;

        _client.SetSpotTableFood();
        _eatTime = _client.Order.Food.TimeToEat;
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        if (_nowTime < _eatTime)
            _nowTime += Time.deltaTime;
        else
            _client.PayAndGoAway();
    }
}
