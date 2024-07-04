using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CafeSpot))]
public class ClientsHolder : MonoBehaviour {
    [SerializeField] private Slider _waitSlider;
    [SerializeField, Min(0)] private float _minTalk;
    [SerializeField, Min(0)] private float _maxTalk;
    [SerializeField, Min(0)] private float _clientWaitMultiplier = 0.75f;

    private SpotData _data;
    private CafeSpot _spot;
    private readonly List<Client> _clients = new();

    public event Action WaitStarted;
    public event Action<CafeSpot> ClientsLeaved;

    private void Awake() {
        _spot = GetComponent<CafeSpot>();
        ChangeSliderState(false);
    }

    private void Update() {
        if (_data.GroupState != GroupClientState.Wait &&
            _data.GroupState != GroupClientState.Talk)
            return;

        if (_data.NowTime < _data.WaitTime) {
            _data.NowTime += InGameTime.Instance.DeltaTime;
            _waitSlider.value = _data.WaitTime - _data.NowTime;
        } else {
            _data.NowTime = 0;
            EndVisit();
            StartCoroutine(ClientsLeave());
        }
    }

    public void AddClient(Client newClient) {
        _clients.Add(newClient);
    }

    public IEnumerator SpawnGroupOfClients() {
        _data.TalkIndex = _clients.Count;
        float spawn = _clients[0].Spawner.SpawnPoint.x;
        RandomizeClients();

        foreach (var client in _clients) {
            client.enabled = true;
            if (client.transform.position.x == spawn) {
                client.StartNewCycle();
                yield return new WaitForSeconds(Random.Range(0.5f, 1.2f));
            }
        }
    }

    public void CheckWait() {
        bool allClientsHere = _data.Clients.All(x => x.State == ClientState.Wait);
        if (allClientsHere)
            StartWait();
    }

    private void StartWait() {
        _data.GroupState = GroupClientState.Wait;
        if (_data.WaitTime == 0)
            _data.WaitTime = _clients[0].ClientData.WaitTime;
        _data.WaitTime *= Mathf.Max(1, _clients.Count * _clientWaitMultiplier);
        _waitSlider.maxValue = _data.WaitTime;

        ChangeSliderState(true);
        WaitStarted?.Invoke();
    }

    public IEnumerator ClientsLeave() {
        RandomizeClients();
        WaitStarted = null;

        Client[] leaveClients = _clients.ToArray();
        _clients.Clear();

        for (int i = 0; i < leaveClients.Length; i++) {
            leaveClients[i].ClientData.State = ClientState.Leave;
        }

        for (int i = 0; i < leaveClients.Length; i++) {
            leaveClients[i].Leave();
            yield return new WaitForSeconds(Random.Range(0.2f, 1f));
        }
    }

    private void RandomizeClients() {
        for (int i = 0; i < _clients.Count - 1; i++) {
            int r = Random.Range(0, _clients.Count);
            (_clients[r], _clients[i]) = (_clients[i], _clients[r]);
        }
    }

    public void EndlessWait() {
        ChangeSliderState(false);
        _data.GroupState = GroupClientState.EndlessWait;
        _data.NowTime = 0;
    }

    public void AddMoney(int money) {
        _data.MoneyCount += money;
    }

    public void CheckTalk() {
        bool allClientsWait = _data.Clients.All(x => x.State == ClientState.Wait);
        if (allClientsWait)
            StartTalking();
    }

    public void DecreaseTalk() {
        _data.TalkIndex--;
        CheckTalk();
    }

    private void StartTalking() {
        _data.GroupState = GroupClientState.Talk;
        if (_data.TalkIndex == 0 || _clients.Count == 1) {
            EndVisit();
            StartCoroutine(ClientsLeave());
            return;
        }

        _data.WaitTime = _data.TalkIndex * Random.Range(_minTalk, _maxTalk);
        _waitSlider.maxValue = _data.WaitTime;
        ChangeSliderState(true);
    }

    private void ChangeSliderState(bool newState) {
        _waitSlider.gameObject.SetActive(newState);
    }

    public void CafeClosed() {
        if (_clients.Count == 0)
            return;

        EndVisit();
        _data.NowTime = 0;
        _clients.Clear();
        StopAllCoroutines();
    }

    private void PayToPlayer() {
        MoneyManager.Instance.ChangeMoney(_data.MoneyCount);
        _data.MoneyCount = 0;
    }

    private void EndVisit() {
        ChangeSliderState(false);
        ClientsLeaved?.Invoke(_spot);
        ClientsLeaved = null;
        _waitSlider.value = 0;
        if (_data.GroupState == GroupClientState.Talk)
            PayToPlayer();
        _data.GroupState = GroupClientState.None;
    }

    public void SetData(SpotData spot) {
        _data = spot;
    }
}
