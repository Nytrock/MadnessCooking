public class ClientEatState : ClientBaseState {
    private ClientData _clientData;

    public override void EnterState(Client client) {
        var clientUI = client.ClientUI;
        client.SetSpotTableFood();
        clientUI.StartEat();

        _clientData = client.ClientData;
    }

    public override void ExitState(Client client) {
        client.ResetSpotTableFood();
    }

    public override void UpdateState(Client client) {
        if (_clientData.NowTime < _clientData.WaitTime)
            _clientData.NowTime += InGameTime.Instance.DeltaTime;
        else
            client.Pay();
        client.ClientUI.UpdateSlider();
    }
}
