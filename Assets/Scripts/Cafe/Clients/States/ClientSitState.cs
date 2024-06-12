public class ClientSitState : ClientBaseState {
    public override void EnterState(Client client) {
        client.ClientUI.ChangeFoodChoiceState(client.ClientData.State == ClientState.Sit);
    }

    public override void ExitState(Client client) {

    }

    public override void UpdateState(Client client) {

    }
}