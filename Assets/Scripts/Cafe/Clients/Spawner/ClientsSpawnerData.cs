using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ClientsSpawnerData {
    [SerializeField] List<ClientData> _leavingClients;
    [SerializeField] bool _isSpawning;
    [SerializeField] float _nowSpawnTime;
    [SerializeField] float _needSpawnTime;

    public IEnumerable<ClientData> LeavingClients => _leavingClients;
    public bool IsSpawning => _isSpawning;
    public float NowSpawnTime => _nowSpawnTime;
    public float NeedSpawnTime => _needSpawnTime;

    public ClientsSpawnerData() {
        _leavingClients = new();
        _isSpawning = true;
    }

    public void AddTime() {
        _nowSpawnTime += InGameTime.Instance.DeltaTime;
    }

    public void SetNewTime(float minTime, float maxTime) {
        _needSpawnTime = Random.Range(minTime, maxTime);
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
}
