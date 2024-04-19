using System;

[Serializable]
public class SerializableClient
{
    public ClientType Type;
    public ClientCount Count;
    public ClientState State;
    public SerializableVector Position;
    public float WaitTime;
    public float NowTime;
    public float WaitMultiplier;
    public Food OrderFood;
    public bool OrderActivated;

    public SerializableClient(Client client, ClientType clientType, ClientCount clientCount, 
        float waitMultiplier, Food food)
    {
        Type = clientType;
        Count = clientCount;
        State = ClientState.Spawn;
        Position = new SerializableVector(client.transform.position);
        WaitMultiplier = waitMultiplier;
        OrderFood = food;
    }
}
