using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class ClientEatState : ClientBaseState {
        private ClientData _clientData;

        public override void EnterState(Client client) {
            client.SetSpotTableFood();
            client.ClientUI.StartEat();
            _clientData = client.Data;
        }

        public override void ExitState(Client client) {
            client.StopEat();
        }

        public override void UpdateState(Client client) {
            _clientData.UpdateTime();
            if (_clientData.NowTime > _clientData.WaitTime)
                client.EndEat();
        }
    }
}
