using System;

[Serializable]
public class SerializableSpot
{
    public bool HaveClients;
    public GroupClientState GroupState;
    public float WaitTime;
    public float NowTime;
    public int TalkIndex;
    public int MoneyCount;
    public int SeatsCount;
    public SerializableClient[] Clients;

    public SerializableSpot(int seatsCount)
    {
        SeatsCount = seatsCount;
        Clients = new SerializableClient[SeatsCount];
        GroupState = GroupClientState.None;
    }

    public void ClearClients()
    {
        Clients = new SerializableClient[SeatsCount];
    }
}