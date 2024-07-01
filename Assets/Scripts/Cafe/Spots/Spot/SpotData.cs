using System;

[Serializable]
public class SpotData {
    public bool AvailableClients;
    public GroupClientState GroupState;
    public float WaitTime;
    public float NowTime;
    public int TalkIndex;
    public int MoneyCount;
    public int SeatsCount;
    public ClientData[] Clients;

    public SpotData(int seatsCount) {
        SeatsCount = seatsCount;
        Clients = new ClientData[SeatsCount];
        GroupState = GroupClientState.None;
    }

    public void ClearClients() {
        AvailableClients = false;
        Clients = new ClientData[SeatsCount];
    }
}