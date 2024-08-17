using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ClientsSpawnerData {
    [SerializeField] private List<ClientData> _leavingClients;
    [SerializeField] private bool _isSpawning;
    [SerializeField] private float _nowSpawnTime;
    [SerializeField] private float _needSpawnTime;
    [SerializeField] private int _servicedClientsCount;

    public IEnumerable<ClientData> LeavingClients => _leavingClients;
    public bool IsSpawning => _isSpawning;
    public float NowSpawnTime => _nowSpawnTime;
    public float NeedSpawnTime => _needSpawnTime;

    public ClientsSpawnerData(float spawnTime) {
        _leavingClients = new();
        _isSpawning = true;
        SetSpawnTime(spawnTime);
    }

    public void AddTime() {
        _nowSpawnTime += InGameTime.Instance.NormalizedDeltaTime;
    }

    public void SetSpawnTime(float spawnTime) {
        _needSpawnTime = spawnTime;
        _nowSpawnTime = 0;
    }

    public void ChangeSpawnMode() {
        _isSpawning = !_isSpawning;
    }

    public void AddLeavingClient(ClientData clientData) {
        _isSpawning = true;
        _leavingClients.Add(clientData);
    }

    public void TryRemoveLeavingClient(ClientData clientData) {
        if (_leavingClients.Contains(clientData))
            _leavingClients.Remove(clientData);
    }

    public void AddServicedClient() {
        _servicedClientsCount++;
    }
}
