using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SpotData {
    [SerializeField] private bool _haveClients;
    [SerializeField] private GroupClientState _groupState;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _nowTime;
    [SerializeField] private int _talkIndex;
    [SerializeField] private int _moneyCount;
    [SerializeField] private int _seatsCount;
    [SerializeField] private ClientData[] _clients;

    public bool HaveClients => _haveClients;
    public GroupClientState GroupState => _groupState;
    public float WaitTime => _waitTime;
    public float NowTime => _nowTime;
    public int TalkIndex => _talkIndex;
    public int SeatsCount => _seatsCount;
    public IEnumerable<ClientData> Clients => _clients;

    public SpotData(int seatsCount) {
        _seatsCount = seatsCount;
        _clients = new ClientData[SeatsCount];
        _groupState = GroupClientState.None;
    }

    public void SetClient(int index, ClientData clientData) {
        _clients[index] = clientData;
    }

    public ClientData GetClient(int index) {
        return _clients[index];
    }

    public void ClearClients() {
        _haveClients = false;
        _clients = new ClientData[SeatsCount];
    }

    public void SetupOnSpawn() {
        _talkIndex = _clients.Length;
        _haveClients = true;
    }

    public void UpdateTime() {
        _nowTime += InGameTime.Instance.NormalizedDeltaTime;
    }

    public void EndVisit() {
        _groupState = GroupClientState.None;
        _nowTime = 0;
        _waitTime = 0;
    }

    public void StartWait(float clientWaitMultiplier) {
        _groupState = GroupClientState.Wait;
        if (_waitTime == 0)
            _waitTime = _clients[0].WaitTime;
        _waitTime *= Mathf.Max(1, _clients.Length * clientWaitMultiplier);
    }

    public void AddMoney(int money) {
        _moneyCount += money;
    }

    public void PayToPlayer() {
        MoneyManager.Instance.ChangeMoney(_moneyCount);
        _moneyCount = 0;
    }

    public void StartEndlessWait() {
        _groupState = GroupClientState.EndlessWait;
        _nowTime = 0;
    }

    public void StartTalk(float minTalkTime, float maxTalkTime) {
        _groupState = GroupClientState.Talk;
        _waitTime = _talkIndex * Random.Range(minTalkTime, maxTalkTime);
    }

    public void DecreaseTalk() {
        _talkIndex--;
    }
}