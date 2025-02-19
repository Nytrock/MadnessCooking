public class ClientWaitState : ClientBaseState {
    public override void EnterState(Client client) {
        client.ClientUI.ChangeFoodChoiceState(client.Data.State == ClientState.WaitOrder);
    }

    public override void ExitState(Client client) {

    }

    public override void UpdateState(Client client) {

    }
}