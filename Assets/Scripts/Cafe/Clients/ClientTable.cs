using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CafeSpot))]
public class ClientTable : MonoBehaviour
{
    [SerializeField] private Slider _talkSlider;
    [SerializeField] private int _minTalk;
    [SerializeField] private int _maxTalk;
    private float _talkTime;
    private float _nowTime;
    private bool _isTalk;
    private int _talkIndex;
    private bool _isWait = true;

    private int _clientsLeft = 0;
    private int _moneyCount = 0;
    private List<Client> _clients = new List<Client>();

    private CafeSpot _spot;

    public event Action<CafeSpot> ClientsLeaved;

    private void Start()
    {
        _spot = GetComponent<CafeSpot>();
        ChangeSlider();
    }

    private void Update()
    {
        if (!_isTalk)
            return;

        if (_nowTime < _talkTime) {
            _nowTime += Time.deltaTime;
            _talkSlider.value = _nowTime;
        } else {
            _nowTime = 0;
            EndVisit();
            ChangeSlider();
            StartCoroutine(ClientsLeave());
        }
    }

    public void AddClient(Client newClient)
    {
        _clients.Add(newClient);
        _clientsLeft++;
    }

    public IEnumerator SpawnGroupOfClients()
    {
        _talkIndex = _clients.Count;
        RandomizeClients();
        for (int i = 0; i < _clients.Count; i++) {
            _clients[i].StartNewCycle();
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.2f));
        }
    }

    public IEnumerator ClientsLeave()
    {
        RandomizeClients();
        for (int i = 0; i < _clients.Count; i++) {
            _clients[i].Leave();
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 1f));
        }
        _clients.Clear();
    }

    private void RandomizeClients()
    {
        var count = _clientsLeft;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = _clients[i];
            _clients[i] = _clients[r];
            _clients[r] = tmp;
        }
    }

    public void CheckWait()
    {
        if (_isWait)  {
            for (int i = 0; i < _clients.Count; i++)
                _clients[i].DisableWait();
            _isWait = false;
        }

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
    }

    private void StartTalking()
    {
        if (_talkIndex == 0) {
            EndVisit();
            StartCoroutine(ClientsLeave());
            return;
        }

        _talkTime = _talkIndex * UnityEngine.Random.Range(_minTalk, _maxTalk);
        _talkSlider.maxValue = _talkTime;
        _isTalk = true;
        ChangeSlider();
    }

    private void ChangeSlider()
    {
        _talkSlider.gameObject.SetActive(_isTalk);
    }

    public void CafeClosed()
    {
        EndVisit();
        _nowTime = 0;
        _clients.Clear();
        ChangeSlider();
        StopAllCoroutines();
    }

    private void PayToPlayer()
    {
        MoneyManager.instance.ChangeMoney(_moneyCount);
        _moneyCount = 0;
    }

    private void EndVisit()
    {
        ClientsLeaved?.Invoke(_spot);
        ClientsLeaved = null;
        if (_isTalk)
            PayToPlayer();
        _isTalk = false;
        _isWait = true;
    }
}
