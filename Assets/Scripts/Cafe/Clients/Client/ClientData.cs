using System;
using UnityEngine;

[Serializable]
public class ClientData {
    public ClientType Type;
    public ClientCount Count;
    public ClientState State;
    public SerializableVector Position;
    public float WaitTime;
    public float NowTime;
    public float WaitMultiplier;
    public Order Order;

    public ClientData(Vector3 position, ClientType clientType, ClientCount clientCount,
        float waitMultiplier, Order order) {
        Type = clientType;
        Count = clientCount;
        State = ClientState.Spawn;
        Position = new SerializableVector(position);
        WaitMultiplier = waitMultiplier;
        Order = order;
    }
}
