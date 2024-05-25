using UnityEngine;
using UnityEngine.UI;

public class ClientWaitState : ClientBaseState
{
    private Slider _waitSlider;
    private SerializableClient _clientData;

    public override void EnterState(Client client)
    {
        var clientUI = client.GetComponent<ClientUI>();

        clientUI.ChangeFoodChoiceState(true);
        clientUI.ChangeSliderState(true);
        _clientData = client.ClientData;
        _waitSlider = clientUI.WaitSlider;
        _waitSlider.maxValue = _clientData.WaitTime;

        if (!_clientData.Order.IsActivated)
            client.OrderActivated += DecreaseWait;
    }

    public override void ExitState(Client client)
    {

    }

    public override void UpdateState(Client client)
    {
        if (_clientData.NowTime < _clientData.WaitTime) {
            _clientData.NowTime += TimeManager.Instance.InGameTimeSpeed;
            _waitSlider.value = _clientData.NowTime;
        } else {
            client.Leave();
        }
    }

    private void DecreaseWait(Client _)
    {
        _clientData.NowTime = Mathf.Max(0, 
            _clientData.NowTime - (_clientData.WaitTime * 0.1f * _clientData.WaitMultiplier));
        _waitSlider.value = _clientData.NowTime;
    }
}
