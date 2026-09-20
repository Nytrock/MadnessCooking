using System;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    [Serializable]
    public struct ClientSettings {
        public ClientData Data { get; private set; }
        public ClientsHolder Holder { get; private set; }
        public CafeSeat Seat { get; private set; }

        public ClientSettings(ClientData data, ClientsHolder holder, CafeSeat seat) {
            Data = data;
            Holder = holder;
            Seat = seat;
        }
    }
}
