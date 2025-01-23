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
    [SerializeField] private RangeFloat _talkTime;
    [SerializeField] private RangeFloat _clientInterval;
    [SerializeField, Min(0)] private float _clientWaitMultiplier = 0.75f;
    [SerializeField, Min(0)] private float _notFullServicePenalty = 0.5f;

    private ClientHolderData _data;
    private CafeSpot _spot;
    private readonly List<Client> _clients = new();
    private bool _isTutorial;

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

        if (_isTutorial)
            return;

        _data.UpdateTime();
        _waitSlider.value = _data.WaitTime - _data.NowTime;

        if (_data.NowTime > _data.WaitTime) {
            EndVisit();
            StartCoroutine(ClientsLeave());
        }
    }

    public void AddClient(Client newClient) {
        newClient.OrderActivated += ActivateAllOrders;
        _clients.Add(newClient);
    }

    private void ActivateAllOrders(Client activatedClient) {
        foreach (var client in _clients) {
            client.OrderActivated -= ActivateAllOrders;
            if (activatedClient != client)
                client.ActivateOrder();
        }
    }

    public IEnumerator SpawnGroupOfClients() {
        _data.SetupOnSpawn();

        float spawn = _clients[0].Spawner.SpawnPoint.x;
        RandomizeClients();

        for (int i = 0; i < _clients.Count; i++) {
            _clients[i].ChangeEnable(true);
            if (_clients[i].transform.position.x == spawn) {
                _clients[i].StartNewCycle();
                if (i != _clients.Count - 1)
                    yield return new WaitForSeconds(_clientInterval.RandomValue);
            }
        }
    }

    public void CheckWait() {
        bool allClientsHere = _data.Clients.All(
            client => client.State == ClientState.Wait
            || client.State == ClientState.Sit
        );

        if (allClientsHere)
            StartWait();
    }

    private void StartWait() {
        if (_data.GroupState == GroupClientState.EndlessWait)
            return;

        ChangeSliderState(true);
        if (_data.GroupState == GroupClientState.Wait) {
            _waitSlider.maxValue = _data.WaitTime;
            return;
        }

        _data.StartWait(_clientWaitMultiplier);
        _waitSlider.maxValue = _data.WaitTime;
        WaitStarted?.Invoke();
    }

    public IEnumerator ClientsLeave(bool noDelay = false) {
        RandomizeClients();

        Client[] leaveClients = _clients.ToArray();
        _clients.Clear();

        for (int i = 0; i < leaveClients.Length; i++)
            leaveClients[i].Data.ChangeState(ClientState.Leave);

        for (int i = 0; i < leaveClients.Length; i++) {
            leaveClients[i].Leave();
            if (_data.GroupState != GroupClientState.Talk)
                leaveClients[i].CheckIsServiced();

            if (noDelay)
                yield return new WaitForSeconds(0);
            else
                yield return new WaitForSeconds(_clientInterval.RandomValue);
        }
    }


    private void RandomizeClients() {
        for (int i = 0; i < _clients.Count - 1; i++) {
            int r = Random.Range(0, _clients.Count);
            (_clients[r], _clients[i]) = (_clients[i], _clients[r]);
        }
    }

    public void StartEndlessWait() {
        ChangeSliderState(false);
        _data.StartEndlessWait();
    }

    public void AddMoney(int money) => _data.AddMoney(money);

    public void CheckTalk() {
        bool allClientsWait = _data.Clients.All(x => x.State == ClientState.Wait);
        if (allClientsWait)
            StartTalk();
    }

    public void DecreaseTalk() {
        _data.DecreaseTalk();
        CheckTalk();
    }

    private void StartTalk() {
        _data.StartTalk(_talkTime.RandomValue);
        if (_data.TalkIndex == 0 || _clients.Count == 1) {
            EndVisit();
            StartCoroutine(ClientsLeave());
            return;
        }

        _waitSlider.maxValue = _data.WaitTime;
        ChangeSliderState(true);
    }

    private void ChangeSliderState(bool newState) {
        _waitSlider.gameObject.SetActive(newState);
    }

    public void CafeStateChanged(bool isOpened) {
        if (_clients.Count == 0 || isOpened)
            return;

        if (_data.GroupState == GroupClientState.Leave)
            return;

        CafeClosed();
    }

    private void CafeClosed() {
        EndVisit();
        StartCoroutine(ClientsLeave(true));
    }

    private void EndVisit() {
        ChangeSliderState(false);
        ClientsLeaved?.Invoke(_spot);
        ClientsLeaved = null;
        WaitStarted = null;
        _waitSlider.value = 0;

        if (_data.GroupState == GroupClientState.Talk)
            _data.PayToPlayer(1);
        else
            _data.PayToPlayer(_notFullServicePenalty);

        _data.EndVisit();
    }

    public void SetData(ClientHolderData spot) {
        _data = spot;
    }

    public void SetTutorialState(bool isTutorial) {
        _isTutorial = isTutorial;
    }
}
