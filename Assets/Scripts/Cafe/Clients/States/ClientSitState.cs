public class ClientSitState : ClientBaseState {
    public override void EnterState(Client client) {
        var clientUI = client.GetComponent<ClientUI>();
        clientUI.ChangeFoodChoiceState(true);
        clientUI.ChangeSliderState(false);
    }

    public override void ExitState(Client client) {

    }

    public override void UpdateState(Client client) {

    }
}