using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CafeSpot))]
public class ClientGroupHolder : MonoBehaviour
{
    [SerializeField] private Slider _waitSlider;
    [SerializeField, Min(0)] private float _minTalk;
    [SerializeField, Min(0)] private float _maxTalk;

    private SpotData _data;
    private CafeSpot _spot;
    private readonly List<GroupClient> _clients = new();

    public event Action WaitStarted;
    public event Action<CafeSpot> ClientsLeaved;

    private void Awake()
    {
        _spot = GetComponent<CafeSpot>();
        ChangeSliderState(false);
    }

    private void Update()
    {
        if (_data.GroupState != GroupClientState.Wait && 
            _data.GroupState != GroupClientState.Talk)
            return;

        if (_data.NowTime < _data.WaitTime) {
            _data.NowTime += TimeManager.Instance.InGameTimeSpeed;
            _waitSlider.value = _data.NowTime;
        } else {
            _data.NowTime = 0;
            EndVisit();
            ChangeSliderState(false);
            StartCoroutine(ClientsLeave());
        }
    }

    public void AddClient(GroupClient newClient)
    {
        _clients.Add(newClient);
    }

    public IEnumerator SpawnGroupOfClients()
    {
        _data.TalkIndex = _clients.Count;
        Vector3 spawn = _clients[0].Spawner.SpawnPoint.position;
        RandomizeClients();

        foreach (var client in _clients) {
            client.enabled = true;
            if (client.transform.position == spawn) {
                client.StartNewCycle();
                yield return new WaitForSeconds(Random.Range(0.5f, 1.2f));
            }
        }
    }

    public void CheckWait()
    {
        bool allClientsHere = _data.Clients.All(x => x.State == ClientState.WaitOthers);
        if (allClientsHere)
            StartWait();
    }

    private void StartWait()
    {
        _data.GroupState = GroupClientState.Wait;
        if (_data.WaitTime == 0)
            _data.WaitTime = _clients[0].ClientData.WaitTime;
        _waitSlider.maxValue = _data.WaitTime;

        ChangeSliderState(true);
        WaitStarted?.Invoke();
    }

    public IEnumerator ClientsLeave()
    {
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

    private void RandomizeClients()
    {
        for (int i = 0; i < _clients.Count - 1; i++) {
            int r = Random.Range(0, _clients.Count);
            (_clients[r], _clients[i]) = (_clients[i], _clients[r]);
        }
    }

    public void EndlessWait()
    {
        ChangeSliderState(false);
        _data.GroupState = GroupClientState.EndlessWait;
        _data.NowTime = 0;
    }

    public void AddMoney(int money)
    {
        _data.MoneyCount += money;
    }

    public void CheckTalk()
    {
        bool allClientsWait = _data.Clients.All(x => x.State == ClientState.WaitOthers);
        if (allClientsWait)
            StartTalking();
    }

    public void DecreaseTalk()
    {
        _data.TalkIndex--;
        CheckTalk();
    }

    private void StartTalking()
    {
        if (_data.TalkIndex == 0) {
            EndVisit();
            StartCoroutine(ClientsLeave());
            return;
        }

        _data.WaitTime = _data.TalkIndex * Random.Range(_minTalk, _maxTalk);
        _waitSlider.maxValue = _data.WaitTime;
        _data.GroupState = GroupClientState.Talk;
        ChangeSliderState(true);
    }

    private void ChangeSliderState(bool newState)
    {
        _waitSlider.gameObject.SetActive(newState);
    }

    public void CafeClosed()
    {
        if (_clients.Count == 0)
            return;

        EndVisit();
        _data.NowTime = 0;
        _clients.Clear();
        ChangeSliderState(false);
        StopAllCoroutines();
    }

    private void PayToPlayer()
    {
        MoneyManager.Instance.ChangeMoney(_data.MoneyCount);
        _data.MoneyCount = 0;
    }

    private void EndVisit()
    {
        ClientsLeaved?.Invoke(_spot);
        ClientsLeaved = null;
        _waitSlider.value = 0;
        if (_data.GroupState == GroupClientState.Talk)
            PayToPlayer();
        _data.GroupState = GroupClientState.None;
    }

    public void SetData(SpotData spot)
    {
        _data = spot;
    }
}
