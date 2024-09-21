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
    [SerializeField, Min(0)] private float _clientWaitMultiplier = 0.75f;
    [SerializeField, Min(0)] private float _minClientInterval = 0.5f;
    [SerializeField, Min(0)] private float _maxClientInterval = 1.2f;

    [SerializeField] private SpotData _data;
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
            if (activatedClient != client)
                client.ActivateOrder();
            client.OrderActivated -= ActivateAllOrders;
        }
    }

    public IEnumerator SpawnGroupOfClients() {
        _data.SetupOnSpawn();

        float spawn = _clients[0].Spawner.SpawnPoint.x;
        RandomizeClients();

        foreach (var client in _clients) {
            client.ChangeEnable(true);
            if (client.transform.position.x == spawn) {
                client.StartNewCycle();
                yield return new WaitForSeconds(Random.Range(_minClientInterval, _maxClientInterval));
            }
        }
    }

    public void CheckWait() {
        bool allClientsHere = _data.Clients.All(x => x.State == ClientState.Wait);
        if (allClientsHere)
            StartWait();
    }

    private void StartWait() {
        _data.StartWait(_clientWaitMultiplier);
        _waitSlider.maxValue = _data.WaitTime;

        ChangeSliderState(true);
        WaitStarted?.Invoke();
    }

    public IEnumerator ClientsLeave() {
        RandomizeClients();

        Client[] leaveClients = _clients.ToArray();
        _clients.Clear();

        for (int i = 0; i < leaveClients.Length; i++)
            leaveClients[i].Data.ChangeState(ClientState.Leave);

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

    public void CafeClosed() {
        if (_clients.Count == 0)
            return;

        EndVisit();
        _clients.Clear();
        _data.ClearClients();
        StopAllCoroutines();
    }

    private void PayToPlayer() => _data.PayToPlayer();

    private void EndVisit() {
        ChangeSliderState(false);
        ClientsLeaved?.Invoke(_spot);
        ClientsLeaved = null;
        WaitStarted = null;
        _waitSlider.value = 0;
        if (_data.GroupState == GroupClientState.Talk)
            PayToPlayer();
        _data.EndVisit();
    }

    public void SetData(SpotData spot) {
        _data = spot;
    }

    public void SetTutorialState(bool isTutorial) {
        _isTutorial = isTutorial;
    }
}
