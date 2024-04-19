using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PopularityXpAdder))]
public class ClientsSpawner : MonoBehaviour, IUpgradeable, IBindable<CafeData>
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PopularityCalculator _popularityCalculate;
    [SerializeField] private CafeOpener _cafeOpener;
    [SerializeField] private CafeSpaceManager _spaceManager;
    [SerializeField] private CafeSpotManager _spotManager;
    [SerializeField] private OrdersManager _ordersManager;
    [SerializeField] private ClientsPool _pool;
    private FoodManager _foodManager;

    [Header("Spawn time")]
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _eatTimeShowUpgrade;

    private readonly List<Client> _clients = new();
    private CafeData _data;
    private PopularityXpAdder _xpAdder;

    public Transform SpawnPoint => _spawnPoint;

    private void Awake()
    {
        _xpAdder = GetComponent<PopularityXpAdder>();
        _cafeOpener.CafeChanged += ChangeWorkMode; 
        _spawnPoint.position += new Vector3(_spaceManager.SpaceCount * _spaceManager.SpaceSize, 0, 0);
        _foodManager = _ordersManager.GetComponent<FoodManager>();
    }

    private void LateStart()
    {
        SetNewTime();
    }

    private void Update()
    {
        if (!_data.IsSpawning || !_data.IsOpened)
            return;

        if (_data.NowSpawnTime < _data.NeedSpawnTime) {
            _data.NowSpawnTime += Time.deltaTime * TimeManager.instance.TimeSpeed;
        } else {
            Spawn();
            SetNewTime();
        }
    }

    private void Spawn()
    {
        var clientCount = GetRandomCount();
        var clientType = GetRandomType(clientCount);
        var spotIndex = _spotManager.TakeRandomSpot(clientCount);
        var waitMultiplier = _popularityCalculate.GetSpaceMultiplier();
        if (spotIndex == -1)
            return;

        var spot = _spotManager.GetSpotByIndex(spotIndex);
        if (clientCount != ClientCount.One) {
            var table = spot.GetComponent<ClientGroupHolder>();
            for (int i = 0; i < spot.SeatsCount; i++) {
                GroupClient client = _pool.GetGroupClient();
                client.ClientEat += ClientEat;
                client.ClientRejected += GroupClientRejected;
                table.AddClient(client);
                client.transform.position = _spawnPoint.position;
                _data.Spots[spotIndex].Clients[i] = new SerializableClient(client, clientType, 
                    clientCount, waitMultiplier, _foodManager.GetRandomFood());
                SetupClient(client, spotIndex, i);
            }
            table.ClientsLeaved += ClientsLeave;
            StartCoroutine(table.SpawnGroupOfClients());
        } else {
            Client client = _pool.GetClient();
            client.transform.position = _spawnPoint.position;
            _data.Spots[spotIndex].Clients[0] = new SerializableClient(client, clientType, 
                clientCount, waitMultiplier, _foodManager.GetRandomFood());
            SetupClient(client, spotIndex, 0);
            client.ClientLeave += ClientLeave;
            client.ClientEat += ClientEat;
            client.StartNewCycle();
        }
        _data.Spots[spotIndex].HaveClients = true;

        if (!_spotManager.CheckHavingSpots())
            ChangeSpawnMode();
    }

    private void SetNewTime()
    {
        var popular = _popularityCalculate.GetPopularity();
        var minTime = _minSpawnTime / popular;
        var maxTime = _maxSpawnTime / popular;

        _data.NeedSpawnTime = Random.Range(minTime, maxTime);
        _data.NowSpawnTime = 0;
    }

    private void ChangeSpawnMode()
    {
        _data.IsSpawning = !_data.IsSpawning;
        SetNewTime();
    }

    private void ChangeWorkMode()
    {
        _data.IsOpened = _cafeOpener.IsOpened;
        SetNewTime();
    }

    private ClientCount GetRandomCount() 
    {
        if (_data.IsWaitingCritic)
            return ClientCount.One;

        _popularityCalculate.GetClientsNumberChances(out int singleChance, out int doubleChance, out int tripleChance, out int quarterChance);
        var number = UnityEngine.Random.Range(1, 1001);
        if (number <= singleChance)
            return ClientCount.One;
        else if (number <= doubleChance)
            return ClientCount.Two;
        else if (number <= tripleChance)
            return ClientCount.Three;
        return ClientCount.Four;
    }

    private ClientType GetRandomType(ClientCount clientCount)
    {
        if (_data.IsWaitingCritic)
            return ClientType.Critic;

        var number = UnityEngine.Random.Range(1, 1001);
        if (number == 1 && clientCount == ClientCount.One)
            return ClientType.GrayMan;
        else if (number <= 50)
            return ClientType.Rich;
        return ClientType.Standard;
    }

    private void ClientLeave(Client client)
    {
        _data.IsSpawning = true;
        _spotManager.ReturnSpot(client.SpotIndex);
        _data.Spots[client.SpotIndex].HaveClients = false;
        _data.LeavingClients.Add(_data.Spots[client.SpotIndex].Clients[0]);
        _data.Spots[client.SpotIndex].ClearClients();
        if (client.ClientData.State != ClientState.Eat)
            _xpAdder.RemoveXp(client.ClientData.Type);
    }

    private void GroupClientRejected(Client client)
    {
        client.ClientLeave -= GroupClientRejected;
        _xpAdder.RemoveXp(client.ClientData.Type);
    }

    private void ClientsLeave(CafeSpot spot)
    {
        _data.IsSpawning = true;
        _spotManager.ReturnSpot(spot.Index);
        _data.Spots[spot.Index].HaveClients = false;
        if (spot.SeatsCount > 1 && !_data.IsOpened) {
            if (_data.Spots[spot.Index].GroupState == GroupClientState.Talk) {
                for (int i = 0; i < spot.SeatsCount; i++) {
                    _xpAdder.RemoveXp(_data.Spots[spot.Index].Clients[i].Type);
                }
            }
        }

        for (int i = 0; i < spot.SeatsCount; i++) {
            _data.LeavingClients.Add(_data.Spots[spot.Index].Clients[i]);
        }
        _data.Spots[spot.Index].ClearClients();
    }

    private void ClientEat(Client client)
    {
        _xpAdder.AddXp(client.ClientData.Type);
    }

    private void SetupClient(Client client, int spotIndex, int tableIndex)
    {
        _clients.Add(client);
        client.ChangeShowingTimeEat(_data.IsEatTimeShow);
        var clientSettings = new ClientSettings(_data.Spots[spotIndex].Clients[tableIndex], 
            spotIndex, tableIndex, this);
        client.Setup(clientSettings);
        _ordersManager.SetNewOrder(client, _spotManager.GetSpotByIndex(spotIndex));
        _cafeOpener.CafeChanged += client.Leave;
    }

    public void ChangeCriticWait(bool newValue)
    {
        _data.IsWaitingCritic = newValue;
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _eatTimeShowUpgrade) {
            _data.IsEatTimeShow = true;
            foreach (var client in _clients) {
                client.ChangeShowingTimeEat(true);
            }
        }
    }

    public void Bind(CafeData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty) {
            LateStart();
            return;
        }

        for (int spotIndex = 0; spotIndex < _data.Spots.Count; spotIndex++) {
            var spotData = _data.Spots[spotIndex];
            if (!spotData.HaveClients)
                continue;

            _spotManager.TakeSpot(spotIndex);
            CafeSpot spot = _spotManager.GetSpotByIndex(spotIndex);
            if (spotData.SeatsCount != 1) {
                var table = spot.GetComponent<ClientGroupHolder>();
                for (int i = 0; i < spotData.SeatsCount; i++) {
                    GroupClient client = _pool.GetGroupClient();
                    client.enabled = true;
                    client.ClientEat += ClientEat;
                    client.ClientRejected += GroupClientRejected;
                    table.AddClient(client);
                    SetupClient(client, spotIndex, i);
                }
                table.ClientsLeaved += ClientsLeave;
                if (spotData.GroupState == GroupClientState.Wait ||
                    spotData.GroupState == GroupClientState.EndlessWait)
                    table.CheckWait();
                else if (spotData.GroupState == GroupClientState.Talk)
                    table.CheckTalk();
            } else {
                Client client = _pool.GetClient();
                SetupClient(client, spotIndex, 0);
                client.ClientLeave += ClientLeave;
                client.ClientEat += ClientEat;
            }
        }

        for (int i = 0; i < _data.LeavingClients.Count; i++) {
            Client client = _pool.GetClient();
            var clientData = _data.LeavingClients[i];
            client.transform.position = clientData.Position.GetVector();
            client.Setup(new ClientSettings(clientData, -1, -1, this));
        }
    }

    public void PutClient(Client client)
    {
        if (_data.LeavingClients.Contains(client.ClientData))
            _data.LeavingClients.Remove(client.ClientData);
        _pool.PutClient(client);
    }

    public CafeSpot GetSpot(int SpotId) => _spotManager.GetSpotByIndex(SpotId);
}
