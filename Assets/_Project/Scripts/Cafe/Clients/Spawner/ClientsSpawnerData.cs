using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class ClientsSpawnerData {
    [SerializeField, JsonProperty] private List<ClientData> _leavingClients;
    [SerializeField, JsonProperty] private bool _isSpawning;
    [SerializeField, JsonProperty] private float _nowSpawnTime;
    [SerializeField, JsonProperty] private float _needSpawnTime;
    [SerializeField, JsonProperty] private int _servicedClientsCount;
    [SerializeField] private int _nowClientsCount = 0;

    public IEnumerable<ClientData> LeavingClients => _leavingClients;
    public bool IsSpawning => _isSpawning;
    public float NowSpawnTime => _nowSpawnTime;
    public float NeedSpawnTime => _needSpawnTime;
    public int NowClientsCount => _nowClientsCount;

    public ClientsSpawnerData() {
        _leavingClients = new();
        _isSpawning = true;
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
        _nowClientsCount--;
        _leavingClients.Add(clientData);
    }

    public void TryRemoveLeavingClient(ClientData clientData) {
        if (_leavingClients.Contains(clientData))
            _leavingClients.Remove(clientData);
    }

    public void AddServicedClient() {
        _servicedClientsCount++;
    }

    public void AddNowClient() {
        _nowClientsCount++;
    }
}
