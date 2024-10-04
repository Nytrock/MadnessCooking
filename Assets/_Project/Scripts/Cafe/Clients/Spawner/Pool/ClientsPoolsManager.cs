using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClientsPoolsManager : MonoBehaviour {
    [SerializeField] private ClientsPool[] _pools;

    public Client GetObject() {
        ClientsPool pool = _pools[Random.Range(0, _pools.Length)];
        return pool.GetObject();
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
