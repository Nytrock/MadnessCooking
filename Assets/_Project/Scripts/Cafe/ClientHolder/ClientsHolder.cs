using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CafeSpot))]
public class ClientsHolder : MonoBehaviour {
    [SerializeField] private Slider _waitSlider;
    [SerializeField] private RangeFloat _waitTime;
    [SerializeField] private RangeFloat _clientInterval;
    [SerializeField, Min(0)] private float _clientServicedTimeBonus;
    [SerializeField, Min(0)] private float _notFullServicePenalty = 0.5f;

    private ClientHolderData _data;
    private CafeSpot _spot;
    private readonly List<Client> _clients = new();

    private bool _isTutorial;
    private int _index;

    public int ClientsCount => _spot.SeatsCount;
    public int Index => _index;
    public ClientHolderData Data => _data;

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

    public CafeSeat GetSeat(int index) => _spot.GetSeat(index);

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

    public IEnumerator SpawnGroupOfClients(ClientSpawnPoint spawnPoint) {
        _data.SetupOnSpawn();

        float spawn = spawnPoint.Position.x;
        _clients.Randomize();

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
        bool allClientsHere = true;
        foreach (var client in _clients) {
            if (client.Data.State == ClientState.Spawn || client.Data.State == ClientState.Leave) {
                allClientsHere = false;
                break;
            }
        }

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

        foreach (var client in _clients)
            client.WaitOrder();

        WaitStarted?.Invoke();
    }

    private IEnumerator ClientsLeave(bool noDelay = false) {
        _clients.Randomize();
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

    public void SetData(ClientHolderData data) {
        _data = data;
    }

    public void SetIndex(int index) {
        _index = index;
    }

    public void SetTutorialState(bool isTutorial) {
        _isTutorial = isTutorial;
        if (_isTutorial)
            _waitSlider.value = 1;
    }
}
