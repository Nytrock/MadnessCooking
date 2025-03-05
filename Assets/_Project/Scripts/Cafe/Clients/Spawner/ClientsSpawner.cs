using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PopularityXpAdder))]
public class ClientsSpawner : MonoBehaviour, IUpgradeable<CafeUpgradeData>, IBindable<CafeData>, ITutorialPart {
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameTimeManager _timeManager;
    [SerializeField] private PopularityCalculator _popularityCalculate;
    [SerializeField] private CafeStateChanger _cafeOpener;
    [SerializeField] private CafeSpaceManager _spaceManager;
    [SerializeField] private CafeSpotManager _spotManager;
    [SerializeField] private OrdersManager _ordersManager;
    [SerializeField] private FoodManager _foodManager;
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private ClientsPoolsManager _pool;
    [SerializeField] private RangeFloat _spawnTime;
    [SerializeField] private float _noClientsMultiplier;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _eatTimeShowUpgrade;

    [SerializeField] private ClientsSpawnerData _data;
    private CriticSpawnerData _criticData;
    private ClientHolderManagerData _clientHolderData;
    private CafeUpgradeData _upgradeData;
    private PopularityXpAdder _xpAdder;

    public event Action PartEnded;

    public Vector2 SpawnPoint => _spawnPoint.position;
    public TutorialManager TutorialManager => _tutorialManager;

    private void Awake() {
        _xpAdder = GetComponent<PopularityXpAdder>();
        _cafeOpener.CafeChanged += delegate { ChangeWorkMode(); };
        _spaceManager.SpaceAdded += MoveSpawnPoint;
    }

    public void LateStart() {
        GenerateClientsFromData();
        SetNewTime();

        _timeManager.DaytimeChanged += CheckDaytime;
    }

    private void CheckDaytime(Daytime daytime) {
        if (daytime == Daytime.Morning || daytime == Daytime.Night)
            SetNewTime();
    }

    private void GenerateClientsFromData() {
        int spotIndex = 0;
        foreach (var spotData in _clientHolderData.ClientHolders) {
            if (!spotData.HaveClients) {
                spotIndex++;
                continue;
            }

            if (spotData.ContainsGrayMan()) {
                spotData.EndVisit();
                spotIndex++;
                continue;
            }

            _spotManager.TakeSpot(spotIndex);
            CafeSpot spot = _spotManager.GetSpotByIndex(spotIndex);
            ClientsHolder table = SpawnGroupOfClients(spot);

            if (spotData.GroupState == GroupClientState.Wait)
                table.CheckWait();
            spotIndex++;
        }

        foreach (var clientData in _data.LeavingClients) {
            Client client = _pool.GetClientByGender(clientData.Gender);
            client.transform.position = clientData.Position.GetVector();
            client.Setup(new ClientSettings(clientData, -1, -1));
        }
    }

    private void Update() {
        if (!_data.IsSpawning || !_cafeOpener.IsOpened || _tutorialManager.IsWork)
            return;

        _data.AddTime();
        if (_data.NowSpawnTime > _data.NeedSpawnTime)
            Spawn();
    }

    private void Spawn() {
        ClientCount clientCount = GetRandomCount();
        int spotIndex = _spotManager.TakeRandomSpot(clientCount);
        if (spotIndex == -1) {
            SetNewTime();
            return;
        }

        ClientType clientType = GetRandomType(clientCount);
        CafeSpot spot = _spotManager.GetSpotByIndex(spotIndex);
        ClientHolderData spotData = _clientHolderData.GetClientHolder(spotIndex);

        for (int i = 0; i < spot.SeatsCount; i++) {
            Order order = new(_foodManager.GetFoodForOrder(), spotIndex + 1);
            ClientData newClient = new(_spawnPoint.position, clientType, order);
            spotData.SetClient(i, newClient);
        }
        SpawnGroupOfClients(spot);

        if (!_spotManager.CheckHavingSpots())
            _data.ChangeSpawnMode();
        SetNewTime();
    }

    private void SetNewTime() {
        float popular = _popularityCalculate.GetPopularity();
        float spawnTime = _spawnTime.RandomValue / popular;

        if (_data.NowClientsCount == 0)
            spawnTime *= _noClientsMultiplier;

        _data.SetSpawnTime(spawnTime);
    }

    private void ChangeWorkMode() {
        SetNewTime();
    }

    private void MoveSpawnPoint() {
        _spawnPoint.position += new Vector3(_spaceManager.SpaceSize, 0, 0);
    }

    private ClientCount GetRandomCount() {
        if (_criticData.IsCriticCanSpawn || _tutorialManager.IsWork)
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
        if (_criticData.IsCriticCanSpawn) {
            _criticData.ChangeCriticSpawn(false);
            return ClientType.Critic;
        }

        int number = Random.Range(1, 1001);
        if (number == 1 && clientCount == ClientCount.One)
            return ClientType.GrayMan;
        else if (number <= 50)
            return ClientType.Rich;
        return ClientType.Standard;
    }

    private void ClientEat(Client client) {
        client.ClientEat -= ClientEat;
        _xpAdder.AddXp(client.Data.Type);
        _data.AddServicedClient();
    }

    private void ClientRejected(Client client) {
        client.ClientRejected -= ClientRejected;
        _xpAdder.RemoveXp(client.Data.Type);
    }

    private void ClientsLeave(ClientsHolder table) {
        _spotManager.ReturnSpot(table.SpotIndex);
        table.ClientsLeaved -= ClientsLeave;

        ClientHolderData spotData = _clientHolderData.GetClientHolder(table.SpotIndex);
        for (int i = 0; i < table.ClientsCount; i++)
            _data.AddLeavingClient(spotData.GetClient(i));
    }

    private void SetupClient(Client client, int spotIndex, int seatIndex) {
        ClientData clientData = _clientHolderData.GetClientHolder(spotIndex).GetClient(seatIndex);
        ClientSettings clientSettings = new(clientData, spotIndex, seatIndex);
        client.Setup(clientSettings);
        _ordersManager.SetNewOrder(client);
        client.EndSetup();
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _eatTimeShowUpgrade)
            _upgradeData.ChangeEatTimeShow(true);
    }

    public void BindUpgrade(CafeUpgradeData upgradeData) {
        _upgradeData = upgradeData;
        _pool.BindUpgrade(upgradeData);
    }

    public void Bind(CafeData data) {
        data.ClientsSpawner ??= new();
        _data = data.ClientsSpawner;

        _clientHolderData = data.ClientHolderManager;
        _criticData = data.CriticSpawner;
    }

    public void PutClient(Client client) {
        _data.TryRemoveLeavingClient(client.Data);
        _pool.PutObject(client);
    }

    private ClientsHolder SpawnGroupOfClients(CafeSpot spot) {
        if (!spot.TryGetComponent(out ClientsHolder table))
            throw new ArgumentNullException("Spot doesn't have the required class ClientGroupHolder");

        table.SetTutorialState(_tutorialManager.IsWork);
        ClientHolderData tableData = _clientHolderData.GetClientHolder(spot.Index);
        table.SetData(tableData);

        for (int i = 0; i < spot.SeatsCount; i++) {
            _data.AddNowClient();
            Client client;
            ClientData clientData = tableData.GetClient(i);

            if (clientData.Gender == ClientGender.None)
                client = _pool.GetClientByType(clientData.Type);
            else
                client = _pool.GetClientByGender(clientData.Gender);

            client.ClientEat += ClientEat;
            client.ClientRejected += ClientRejected;
            table.AddClient(client);
            SetupClient(client, spot.Index, i);
        }

        if (_tutorialManager.IsWork)
            table.WaitStarted += _tutorialManager.NextTutorialPart;
        table.ClientsLeaved += ClientsLeave;
        StartCoroutine(table.SpawnGroupOfClients());
        return table;
    }


    public void StartTutorialPart() {
        Spawn();
        PartEnded?.Invoke();
    }

    public CafeSpot GetSpot(int SpotIndex) => _spotManager.GetSpotByIndex(SpotIndex);
}
