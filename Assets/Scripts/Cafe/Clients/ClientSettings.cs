using System;
using UnityEngine;

[Serializable]
public struct ClientSettings
{
    public ClientSettings(Transform exitTarget, Transform enterTarget, ClientType clientType, CafeSpot spot, int spotIndex, float waitMultiplier, ClientsPool _pool)
    {
        ExitTarget = exitTarget;
        EnterTarget = enterTarget;
        ClientType = clientType;
        Spot = spot;
        SpotIndex = spotIndex;
        WaitMultiplier = waitMultiplier;
        Pool = _pool;
    }

    public Transform ExitTarget { get; private set; }
    public Transform EnterTarget { get; private set; }
    public ClientType ClientType { get; private set; }
    public CafeSpot Spot { get; private set; }
    public int SpotIndex { get; private set; }
    public float WaitMultiplier { get; private set; }
    public ClientsPool Pool { get; private set; }
}
