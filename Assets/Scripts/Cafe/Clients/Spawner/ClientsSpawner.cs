using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PopularityXpAdder))]
public class ClientsSpawner : MonoBehaviour, IUpgradeable<CafeUpgradeData>, IBindable<CafeData> {
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

    [SerializeField] private ClientsSpawnerData _data;
    private CafeSpotManagerData _spotData;
    private CafeUpgradeData _upgradeData;
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
        if (!_data.IsSpawning || !_cafeOpener.IsOpened)
            return;

        if (_data.NowSpawnTime < _data.NeedSpawnTime) {
            _data.AddTime();
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
        SpotData spotData = _spotData.GetSpot(spotIndex);
        Order order;
        for (int i = 0; i < spot.SeatsCount; i++) {
            order = new(_foodManager.GetRandomFood(), spotIndex + 1);
            spotData.Clients[i] = new ClientData(_spawnPoint.position,
                clientType, clientCount, waitMultiplier, order);
        }
        SpawnGroupOfClients(spot);
        spotData.AvailableClients = true;

        if (!_spotManager.CheckHavingSpots())
            ChangeSpawnMode();
    }

    private void SetNewTime() {
        float popular = _popularityCalculate.GetPopularity();
        float minTime = _minSpawnTime / popular;
        float maxTime = _maxSpawnTime / popular;
        _data.SetNewTime(minTime, maxTime);
    }

    private void ChangeSpawnMode() {
        _data.ChangeSpawnMode();
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
        _spotManager.ReturnSpot(spot.Index);

        SpotData spotData = _spotData.GetSpot(spot.Index);
        for (int i = 0; i < spot.SeatsCount; i++)
            _data.AddLeavingClient(spotData.Clients[i]);
    }

    private void SetupClient(Client client, int spotIndex, int tableIndex) {
        client.ClientUI.SetData(_upgradeData);
        ClientData clientData = _spotData.GetSpot(spotIndex).Clients[tableIndex];
        ClientSettings clientSettings = new(clientData, spotIndex, tableIndex, this);
        client.Setup(clientSettings);
        _ordersManager.SetNewOrder(client);
        _cafeOpener.CafeChanged += client.Leave;
    }

    public void ChangeCriticWait(bool newValue) {
        _data.ChangeCriticWait(newValue);
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _eatTimeShowUpgrade)
            _upgradeData.ChangeEatTimeShow(true);
    }

    public void BindUpgrade(CafeUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    public void Bind(CafeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.ClientsSpawner = new();
        _data = data.ClientsSpawner;
        _spotData = data.SpotManager;

        if (isFileEmpty) {
            LateStart();
            return;
        }

        int spotIndex = 0;
        foreach (var spotData in _spotData.Spots) {
            if (!spotData.AvailableClients) {
                spotIndex++;
                continue;
            }

            _spotManager.TakeSpot(spotIndex);
            CafeSpot spot = _spotManager.GetSpotByIndex(spotIndex);
            ClientsHolder table = SpawnGroupOfClients(spot);
            if (spotData.GroupState == GroupClientState.Wait ||
                spotData.GroupState == GroupClientState.EndlessWait)
                table.CheckWait();
            else if (spotData.GroupState == GroupClientState.Talk)
                table.CheckTalk();

            spotIndex++;
        }

        foreach (var clientData in _data.LeavingClients) {
            Client client = _pool.GetObject();
            client.transform.position = clientData.Position.GetVector();
            client.Setup(new ClientSettings(clientData, -1, -1, this));
        }
    }

    public void PutClient(Client client) {
        _data.TryRemoveLeavingClient(client.ClientData);
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
