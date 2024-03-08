using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CafeSpot))]
public class ClientGroupHolder : MonoBehaviour
{
    [SerializeField] private Slider _waitSlider;
    [SerializeField] private float _minTalk;
    [SerializeField] private float _maxTalk;
    private int _talkIndex;
    private bool _isTalk;

    private bool _isWait = true;
    private float _waitTime;
    private float _nowTime;

    private int _clientsLeft;
    private int _moneyCount;
    private readonly List<Client> _clients = new();

    public event Action<Client> ClientsLeaved;
    public event Action<bool> WaitChanged;

    private void Start()
    {
        ChangeSliderState(false);
    }

    private void Update()
    {
        if (!_isWait)
            return;

        if (_nowTime < _waitTime) {
            _nowTime += Time.deltaTime * TimeManager.instance.TimeSpeed;
            _waitSlider.value = _nowTime;
        } else {
            _nowTime = 0;
            EndVisit();
            ChangeSliderState(false);
            StartCoroutine(ClientsLeave());
        }
    }

    public void AddClient(Client newClient)
    {
        _clients.Add(newClient);
    }

    public IEnumerator SpawnGroupOfClients()
    {
        _talkIndex = _clients.Count;
        _clientsLeft = _clients.Count;
        RandomizeClients();

        for (int i = 0; i < _clients.Count; i++) {
            _clients[i].StartNewCycle();
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.2f));
        }
    }

    public void CheckWait()
    {
        if (--_clientsLeft == 0)
            StartWait();
    }

    private void StartWait()
    {
        _isWait = true;
        _waitTime = _clients[0].GetWaitTime();
        _nowTime = 0;

        _waitSlider.maxValue = _waitTime;
        _clientsLeft = _clients.Count;


        ChangeSliderState(_isWait);
        WaitChanged?.Invoke(true);
    }

    public IEnumerator ClientsLeave()
    {
        RandomizeClients();
        WaitChanged?.Invoke(false);
        WaitChanged = null;

        Client[] leaveClients = _clients.ToArray();
        _clients.Clear();

        for (int i = 0; i < leaveClients.Length; i++) {
            leaveClients[i].Leave();
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 1f));
        }
    }

    private void RandomizeClients()
    {
        var last = _clients.Count - 1;
        for (var i = 0; i < last; ++i) {
            var r = UnityEngine.Random.Range(i, _clients.Count);
            (_clients[r], _clients[i]) = (_clients[i], _clients[r]);
        }
    }

    public void EndlessWait()
    {
        ChangeSliderState(false);
        _isWait = false;
    }

    public void AddMoney(int money)
    {
        _moneyCount += money;
    }

    public void CheckTalk()
    {
        if (--_clientsLeft == 0)
            StartTalking();
    }

    public void DecreaseTalk()
    {
        _talkIndex--;
        CheckTalk();
    }

    private void StartTalking()
    {
        if (_talkIndex == 0) {
            EndVisit();
            StartCoroutine(ClientsLeave());
            return;
        }

        _waitTime = _talkIndex * UnityEngine.Random.Range(_minTalk, _maxTalk);
        _waitSlider.maxValue = _waitTime;
        _isWait = true;
        _isTalk = true;
        ChangeSliderState(true);
    }

    private void ChangeSliderState(bool newState)
    {
        _waitSlider.gameObject.SetActive(newState);
    }

    public void CafeClosed()
    {
        EndVisit();
        _nowTime = 0;
        _clients.Clear();
        ChangeSliderState(false);
        StopAllCoroutines();
    }

    private void PayToPlayer()
    {
        MoneyManager.instance.ChangeMoney(_moneyCount);
        _moneyCount = 0;
    }

    private void EndVisit()
    {
        ClientsLeaved?.Invoke(_clients[0]);
        ClientsLeaved = null;
        _waitSlider.value = 0;
        if (_isTalk)
            PayToPlayer();
        _isTalk = false;
        _isWait = false;
    }
}
