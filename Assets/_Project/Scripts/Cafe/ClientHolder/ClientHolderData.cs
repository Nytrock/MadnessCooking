using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class ClientHolderData {
    [SerializeField, JsonProperty] private bool _haveClients;
    [SerializeField, JsonProperty] private GroupClientState _groupState;
    [SerializeField, JsonProperty] private float _waitTime;
    [SerializeField, JsonProperty] private float _nowTime;
    [SerializeField, JsonProperty] private int _talkIndex;
    [SerializeField, JsonProperty] private int _moneyCount;
    [SerializeField, JsonProperty] private int _seatsCount;
    [SerializeField, JsonProperty] private ClientData[] _clients;

    public bool HaveClients => _haveClients;
    public GroupClientState GroupState => _groupState;
    public float WaitTime => _waitTime;
    public float NowTime => _nowTime;
    public int TalkIndex => _talkIndex;
    public int SeatsCount => _seatsCount;
    public IEnumerable<ClientData> Clients => _clients;

    public ClientHolderData(int seatsCount) {
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

    private void ClearClients() {
        _haveClients = false;
        _clients = new ClientData[SeatsCount];
    }

    public void StopWait() {
        _groupState = GroupClientState.Serviced;
    }

    public void SetupOnSpawn() {
        if (_groupState == GroupClientState.Leave || _groupState == GroupClientState.None) {
            _groupState = GroupClientState.Enter;
            _talkIndex = _clients.Length;
            _haveClients = true;
        }
    }

    public void UpdateTime() {
        _nowTime += InGameTime.Instance.NormalizedDeltaTime;
    }

    public void EndVisit() {
        _groupState = GroupClientState.Leave;
        _nowTime = 0;
        _waitTime = 0;
        ClearClients();
    }

    public void StartWait(float waitTime) {
        _groupState = GroupClientState.Wait;
        if (_waitTime == 0)
            _waitTime = waitTime;
    }

    public void AddMoney(int money) {
        _moneyCount += money;
    }

    public void PayToPlayer(float coeficient) {
        int payingMoney = Mathf.FloorToInt(_moneyCount * coeficient);
        MoneyManager.Instance.ChangeMoney(payingMoney);
        _moneyCount = 0;
    }

    public void DecreaseTalk() {
        _talkIndex--;
    }

    public bool ContainsGrayMan() {
        return _clients.Length == 1 && _clients[0].Type == ClientType.GrayMan;
    }

    public void AddWaitTime(float time) {
        _nowTime = Mathf.Max(_nowTime - time, 0);
    }
}