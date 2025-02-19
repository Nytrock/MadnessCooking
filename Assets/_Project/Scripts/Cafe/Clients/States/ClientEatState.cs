public class ClientEatState : ClientBaseState {
    private ClientData _clientData;

    public override void EnterState(Client client) {
        var clientUI = client.ClientUI;
        client.SetSpotTableFood();
        clientUI.StartEat();

        _clientData = client.Data;
    }

    public override void ExitState(Client client) {
        client.ClientUI.ChangeEatSliderState(false);
        client.ResetSpotTableFood();
    }

    public override void UpdateState(Client client) {
        _clientData.UpdateTime();
        if (_clientData.NowTime > _clientData.WaitTime)
            client.EndEat();
        client.ClientUI.UpdateSlider();
    }
}
