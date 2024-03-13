using UnityEngine;
using UnityEngine.UI;

public class ClientEatingState : ClientState
{
    private Slider _eatSlider;
    private float _eatTime;
    private float _nowTime = 0;

    public override void EnterState(Client client)
    {
        var clientUI = client.GetComponent<ClientUI>();
        client.SetSpotTableFood();

        clientUI.ChangeFoodChoiceState(false);
        clientUI.ChangeSliderState(client.IsEatTimeShow);

        _eatTime = client.Order.Food.TimeToEat;
        _nowTime = 0;
        _eatSlider = clientUI.WaitSlider;
        _eatSlider.maxValue = _eatTime;
    }

    public override void ExitState(Client client)
    {
        client.ResetSpotTableFood();
    }

    public override void UpdateState(Client client)
    {
        if (_nowTime < _eatTime) {
            _nowTime += Time.deltaTime * TimeManager.instance.TimeSpeed;
            _eatSlider.value = _nowTime;
        } else {
            client.Pay();
        }
    }
}
