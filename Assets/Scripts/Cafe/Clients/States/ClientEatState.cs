using UnityEngine.UI;

public class ClientEatState : ClientBaseState {
    private Slider _eatSlider;
    private ClientData _clientData;

    public override void EnterState(Client client) {
        var clientUI = client.GetComponent<ClientUI>();
        client.SetSpotTableFood();
        clientUI.ChangeFoodChoiceState(false);
        clientUI.ChangeSliderState(true);

        _clientData = client.ClientData;
        _eatSlider = clientUI.WaitSlider;
        _eatSlider.maxValue = _clientData.WaitTime;
    }

    public override void ExitState(Client client) {
        client.ResetSpotTableFood();
    }

    public override void UpdateState(Client client) {
        if (_clientData.NowTime < _clientData.WaitTime) {
            _clientData.NowTime += InGameTime.Instance.DeltaTime;
            _eatSlider.value = _clientData.NowTime;
        } else {
            client.Pay();
        }
    }
}
