using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PopularityXpAdder))]
public class ClientsSpawner : MonoBehaviour, IUpgradeable, IBindable<CafeData> {
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PopularityCalculator _popularityCalculate;
    [SerializeField] private CafeOpener _cafeOpener;
    [SerializeField] private CafeSpaceManager _spaceManager;
    [SerializeField] private CafeSpotManager _spotManager;
    [SerializeField] private OrdersManager _ordersManager;
    [SerializeField] private FoodManager _foodManager;
    [SerializeField] private ClientsPool _pool;

    [Header("Spawn time")]
    [SerializeField, Min(0)] private float _minSpawnTime;
    [SerializeField, Min(0)] private float _maxSpawnTime;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _eatTimeShowUpgrade;

    private CafeData _data;
    private PopularityXpAdder _xpAdder;

    public Vector2 SpawnPoint => _spawnPoint.position;

    private void Awake() {
        _xpAdder = GetComponent<PopularityXpAdder>();
        _cafeOpener.CafeChanged += ChangeWorkMode;
        _spaceManager.SpaceAdded += MoveSpawnPoint;
    }

    private void MoveSpawnPoint() {
        _spawnPoint.position += new Vector3(_spaceManager.SpaceSize, 0, 0);
    }

    private void LateStart() {
        SetNewTime();
    }

    private void Update() {
        if (!_data.IsSpawning || !_data.IsOpened)
            return;

        if (_data.NowSpawnTime < _data.NeedSpawnTime) {
            _data.NowSpawnTime += InGameTime.Instance.DeltaTime;
        } else {
            Spawn();
            SetNewTime();
        }
    }

    private void Spawn() {
        ClientCount clientCount = GetRandomCount();
        int spotIndex = _spotManager.TakeRandomSpot(clientCount);
        if (spotIndex == -1)
            return;

        float waitMultiplier = _popularityCalculate.GetSpaceMultiplier();
        ClientType clientType = GetRandomType(clientCount);
        CafeSpot spot = _spotManager.GetSpotByIndex(spotIndex);
        Order order;
        for (int i = 0; i < spot.SeatsCount; i++) {
            order = new(_foodManager.GetRandomFood(), spotIndex + 1);
            _data.Spots[spotIndex].Clients[i] = new ClientData(_spawnPoint.position,
                clientType, clientCount, waitMultiplier, order);
        }
        SpawnGroupOfClients(spot);
        _data.Spots[spotIndex].AvailableClients = true;

        if (!_spotManager.CheckHavingSpots())
            ChangeSpawnMode();
    }

    private void SetNewTime() {
        float popular = _popularityCalculate.GetPopularity();
        float minTime = _minSpawnTime / popular;
        float maxTime = _maxSpawnTime / popular;

        _data.NeedSpawnTime = Random.Range(minTime, maxTime);
        _data.NowSpawnTime = 0;
    }

    private void ChangeSpawnMode() {
        _data.IsSpawning = !_data.IsSpawning;
        SetNewTime();
    }

    private void ChangeWorkMode() {
        SetNewTime();
    }

    private ClientCount GetRandomCount() {
        if (_data.IsWaitingCritic)
            return ClientCount.One;

        _popularityCalculate.GetClientChances(out int singleChance, out int doubleChance, out int tripleChance, out int quarterChance);
        int chance = Random.Range(1, 1001);
        if (chance <= singleChance)
            return ClientCount.One;
        else if (chance <= doubleChance)
            return ClientCount.Two;
        else if (chance <= tripleChance)
            return ClientCount.Three;
        return ClientCount.Four;
    }

    private ClientType GetRandomType(ClientCount clientCount) {
        if (_data.IsWaitingCritic)
            return ClientType.Critic;

        int number = Random.Range(1, 1001);
        if (number == 1 && clientCount == ClientCount.One)
            return ClientType.GrayMan;
        else if (number <= 50)
            return ClientType.Rich;
        return ClientType.Standard;
    }

    private void ClientEat(Client client) {
        client.ClientEat -= ClientEat;
        _xpAdder.AddXp(client.ClientData.Type);
    }

    private void ClientRejected(Client client) {
        client.ClientRejected -= ClientRejected;
        _xpAdder.RemoveXp(client.ClientData.Type);
    }

    private void ClientsLeave(CafeSpot spot) {
        _data.IsSpawning = true;
        _spotManager.ReturnSpot(spot.Index);

        SpotData spotData = _data.Spots[spot.Index];
        if (spot.SeatsCount > 1 && !_data.IsOpened) {
            if (spotData.GroupState == GroupClientState.Talk) {
                for (int i = 0; i < spot.SeatsCount; i++) {
                    _xpAdder.RemoveXp(spotData.Clients[i].Type);
                }
            }
        }

        for (int i = 0; i < spot.SeatsCount; i++) {
            _data.LeavingClients.Add(spotData.Clients[i]);
        }
        spotData.ClearClients();
    }

    private void SetupClient(Client client, int spotIndex, int tableIndex) {
        client.ClientUI.SetData(_data);
        ClientData clientData = _data.Spots[spotIndex].Clients[tableIndex];
        ClientSettings clientSettings = new(clientData, spotIndex, tableIndex, this);
        client.Setup(clientSettings);
        _ordersManager.SetNewOrder(client);
        _cafeOpener.CafeChanged += client.Leave;
    }

    public void ChangeCriticWait(bool newValue) {
        _data.IsWaitingCritic = newValue;
    }

    public void CheckUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _eatTimeShowUpgrade)
            _data.IsEatTimeShow = true;
    }

    public void Bind(CafeData data, bool isFileEmpty) {
        _data = data;
        if (isFileEmpty) {
            LateStart();
            return;
        }

        for (int spotIndex = 0; spotIndex < _data.Spots.Count; spotIndex++) {
            SpotData spotData = _data.Spots[spotIndex];
            if (!spotData.AvailableClients)
                continue;

            _spotManager.TakeSpot(spotIndex);
            CafeSpot spot = _spotManager.GetSpotByIndex(spotIndex);
            ClientsHolder table = SpawnGroupOfClients(spot);
            if (spotData.GroupState == GroupClientState.Wait ||
                spotData.GroupState == GroupClientState.EndlessWait)
                table.CheckWait();
            else if (spotData.GroupState == GroupClientState.Talk)
                table.CheckTalk();
        }

        for (int i = 0; i < _data.LeavingClients.Count; i++) {
            Client client = _pool.GetObject();
            ClientData clientData = _data.LeavingClients[i];
            client.transform.position = clientData.Position.GetVector();
            client.Setup(new ClientSettings(clientData, -1, -1, this));
        }
    }

    public void PutClient(Client client) {
        if (_data.LeavingClients.Contains(client.ClientData))
            _data.LeavingClients.Remove(client.ClientData);
        _pool.PutObject(client);
    }

    private ClientsHolder SpawnGroupOfClients(CafeSpot spot) {
        if (!spot.TryGetComponent(out ClientsHolder table))
            throw new ArgumentNullException("Spot doesn't have the required class ClientGroupHolder");

        for (int i = 0; i < spot.SeatsCount; i++) {
            Client client = _pool.GetObject();
            client.ClientEat += ClientEat;
            client.ClientRejected += ClientRejected;
            table.AddClient(client);
            SetupClient(client, spot.Index, i);
        }
        table.ClientsLeaved += ClientsLeave;
        StartCoroutine(table.SpawnGroupOfClients());
        return table;
    }

    public CafeSpot GetSpot(int SpotIndex) => _spotManager.GetSpotByIndex(SpotIndex);
}
