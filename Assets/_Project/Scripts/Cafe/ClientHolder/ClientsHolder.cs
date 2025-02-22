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
    [SerializeField] private RangeFloat _waitTime;
    [SerializeField] private RangeFloat _clientInterval;
    [SerializeField, Min(0)] private float _clientServicedTimeBonus;
    [SerializeField, Min(0)] private float _notFullServicePenalty = 0.5f;

    private ClientsSpawner _spawner;
    private ClientHolderData _data;
    private CafeSpot _spot;
    private readonly List<Client> _clients = new();
    private bool _isTutorial;

    public int ClientsCount => _spot.SeatsCount;
    public int SpotIndex => _spot.Index;

    public event Action WaitStarted;
    public event Action<ClientsHolder> ClientsLeaved;

    private void Awake() {
        _spot = GetComponent<CafeSpot>();
        ChangeSliderState(false);
    }

    private void Update() {
        if (_data.GroupState != GroupClientState.Wait)
            return;

        if (_isTutorial)
            return;

        _data.UpdateTime();
        _waitSlider.value = _data.WaitTime - _data.NowTime;

        if (_data.NowTime > _data.WaitTime)
            EndVisit();
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

        float spawn = _spawner.SpawnPoint.x;
        RandomizeClients();

        for (int i = 0; i < _clients.Count; i++) {
            _clients[i].ChangeEnable(true);
            if (_clients[i].transform.position.x == spawn) {
                _clients[i].StartNewCycle();
                if (i != _clients.Count - 1)
                    yield return new WaitForSeconds(_clientInterval.RandomValue / InGameTime.Instance.NormalizedTime);
            }
        }
    }

    public void CheckWait() {
        bool allClientsHere = _data.Clients.All(
            client => client.State != ClientState.Spawn
            && client.State != ClientState.Leave
        );

        if (allClientsHere)
            StartWait();
    }

    private void StartWait() {
        if (_data.GroupState == GroupClientState.Serviced)
            return;

        ChangeSliderState(true);
        if (_data.GroupState == GroupClientState.Wait) {
            _waitSlider.maxValue = _data.WaitTime;
            return;
        }

        _data.StartWait(_waitTime.RandomValue);
        _waitSlider.maxValue = _data.WaitTime;
        WaitStarted?.Invoke();
    }

    private IEnumerator ClientsLeave(bool noDelay = false) {
        RandomizeClients();

        Client[] leaveClients = _clients.ToArray();
        _clients.Clear();

        for (int i = 0; i < leaveClients.Length; i++)
            leaveClients[i].Data.ChangeState(ClientState.Leave);

        for (int i = 0; i < leaveClients.Length; i++) {
            leaveClients[i].Leave();
            leaveClients[i].CheckIsServiced();

            if (noDelay)
                yield return new WaitForSeconds(0);
            else
                yield return new WaitForSeconds(_clientInterval.RandomValue / InGameTime.Instance.NormalizedTime);
        }
    }

    private void RandomizeClients() {
        for (int i = 0; i < _clients.Count - 1; i++) {
            int r = Random.Range(0, _clients.Count);
            (_clients[r], _clients[i]) = (_clients[i], _clients[r]);
        }
    }

    public void ClientEat(int money) {
        _data.AddMoney(money);
        _data.AddWaitTime(_clientServicedTimeBonus);
        CheckWaitEnded();
    }

    private void CheckWaitEnded() {
        bool allClientsServed = _data.Clients.All(x => x.State != ClientState.WaitOrder);
        if (!allClientsServed)
            return;

        ChangeSliderState(false);
        _data.StopWait();
    }

    public void CheckVisitEnded() {
        bool allClientsFinish = _data.Clients.All(x => x.State == ClientState.WaitOthers);
        if (!allClientsFinish)
            return;

        EndVisit(true);
    }

    public void FoodRejected() {
        _data.DecreaseTalk();
        CheckWaitEnded();
        CheckVisitEnded();
    }

    private void ChangeSliderState(bool newState) {
        _waitSlider.gameObject.SetActive(newState);
    }

    public void CafeStateChanged(bool isOpened) {
        if (_clients.Count == 0 || isOpened)
            return;

        if (_data.GroupState == GroupClientState.Leave)
            return;

        EndVisit(true);
    }

    private void EndVisit(bool instantLeave = false) {
        ChangeSliderState(false);
        ClientsLeaved?.Invoke(this);
        WaitStarted = null;
        _waitSlider.value = 0;

        if (_data.GroupState == GroupClientState.Serviced)
            _data.PayToPlayer(1);
        else
            _data.PayToPlayer(_notFullServicePenalty);

        _data.EndVisit();
        StartCoroutine(ClientsLeave(instantLeave));
    }

    public void SetData(ClientHolderData spot) {
        _data = spot;
    }

    public void SetTutorialState(bool isTutorial) {
        _isTutorial = isTutorial;
    }

    public void SetSpawner(ClientsSpawner spawner) {
        _spawner = spawner;
    }
}
