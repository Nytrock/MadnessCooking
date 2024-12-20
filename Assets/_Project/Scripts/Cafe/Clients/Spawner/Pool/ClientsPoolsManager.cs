using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClientsPoolsManager : MonoBehaviour {
    [SerializeField] private ClientsPool[] _pools;

    public Client GetClientByType(ClientType clientType) {
        if (clientType == ClientType.GrayMan)
            return GetClientByGender(ClientGender.Male);

        ClientsPool randomPool = _pools[Random.Range(0, _pools.Length)];
        return randomPool.GetObject();
    }

    public Client GetClientByGender(ClientGender gender) {
        foreach (var pool in _pools) {
            if (pool.ClientsGender == gender) {
                return pool.GetObject();
            }
        }

        return null;
    }

    public void PutObject(Client client) {
        foreach (var pool in _pools) {
            if (client.Gender == pool.ClientsGender) {
                pool.PutObject(client);
                return;
            }
        }

        throw new NullReferenceException($"There's no client pool for gender {client.Gender}");
    }
}
