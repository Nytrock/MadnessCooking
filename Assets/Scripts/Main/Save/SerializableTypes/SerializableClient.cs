using System;
using UnityEngine;

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

    public SerializableClient(Vector3 position, ClientType clientType, ClientCount clientCount, 
        float waitMultiplier, Food food)
    {
        Type = clientType;
        Count = clientCount;
        State = ClientState.Spawn;
        Position = new SerializableVector(position);
        WaitMultiplier = waitMultiplier;
        OrderFood = food;
    }
}
