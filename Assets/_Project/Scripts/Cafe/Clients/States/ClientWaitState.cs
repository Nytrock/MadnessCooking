using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class ClientWaitState : ClientBaseState {
        public override void EnterState(Client client) {
            client.ClientUI.ChangeFoodChoiceState(client.Data.State == ClientState.WaitOrder);
        }

        public override void ExitState(Client client) {
            client.ClientUI.ChangeFoodChoiceState(false);
        }

        public override void UpdateState(Client client) {

        }
    }
}