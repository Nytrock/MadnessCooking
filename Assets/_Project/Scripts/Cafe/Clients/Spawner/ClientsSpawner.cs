using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PopularityXpAdder))]
public class ClientsSpawner : MonoBehaviour, IUpgradeable<CafeUpgradeData>, IBindable<CafeData>, ITutorialPart {
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PopularityCalculator _popularityCalculate;
    [SerializeField] private CafeStateChanger _cafeOpener;
    [SerializeField] private CafeSpaceManager _spaceManager;
    [SerializeField] private CafeSpotManager _spotManager;
    [SerializeField] private OrdersManager _ordersManager;
    [SerializeField] private FoodManager _foodManager;
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private ClientsPoolsManager _pool;
    [SerializeField] private RangeFloat _spawnTime;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _eatTimeShowUpgrade;

    [SerializeField] private ClientsSpawnerData _data;
    private CriticSpawnerData _criticData;
    private CafeSpotManagerData _spotData;
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

    private void LateStart() {
        SetNewTime();
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
        if (spotIndex == -1)
            return;

        float waitMultiplier = _popularityCalculate.GetSpaceMultiplier();
        ClientType clientType = GetRandomType(clientCount);
        CafeSpot spot = _spotManager.GetSpotByIndex(spotIndex);
        SpotData spotData = _spotData.GetSpot(spotIndex);

        for (int i = 0; i < spot.SeatsCount; i++) {
            Order order = new(_foodManager.GetRandomFood(), spotIndex + 1);
            ClientData newClient = new(_spawnPoint.position, clientType, waitMultiplier, order);
            spotData.SetClient(i, newClient);
        }
        SpawnGroupOfClients(spot);

        if (!_spotManager.CheckHavingSpots())
            _data.ChangeSpawnMode();
        SetNewTime();
    }

    private void SetNewTime() {
        float popular = _popularityCalculate.GetPopularity();
        _data.SetSpawnTime(_spawnTime.RandomValue / popular);
    }

    private void ChangeWorkMode() {
        SetNewTime();
    }

    private void MoveSpawnPoint() {
        _spawnPoint.position += new Vector3(_spaceManager.SpaceSize, 0, 0);
    }

    private ClientCount GetRandomCount() {
        if (_criticData.IsWaitingCritic || _tutorialManager.IsWork)
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
        if (_criticData.IsWaitingCritic)
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
        _xpAdder.AddXp(client.Data.Type, client.Data.WaitCoef);
        _data.AddServicedClient();
    }

    private void ClientRejected(Client client) {
        client.ClientRejected -= ClientRejected;
        _xpAdder.RemoveXp(client.Data.Type);
    }

    private void ClientsLeave(CafeSpot spot) {
        _spotManager.ReturnSpot(spot.Index);

        SpotData spotData = _spotData.GetSpot(spot.Index);
        for (int i = 0; i < spot.SeatsCount; i++)
            _data.AddLeavingClient(spotData.GetClient(i));
    }

    private void SetupClient(Client client, int spotIndex, int seatIndex) {
        client.ClientUI.SetData(_upgradeData);
        ClientData clientData = _spotData.GetSpot(spotIndex).GetClient(seatIndex);
        ClientSettings clientSettings = new(clientData, spotIndex, seatIndex, this);
        client.Setup(clientSettings);
        _ordersManager.SetNewOrder(client);
        _cafeOpener.CafeChanged += client.CheckCafe;
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _eatTimeShowUpgrade)
            _upgradeData.ChangeEatTimeShow(true);
    }

    public void BindUpgrade(CafeUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    public void Bind(CafeData data) {
        if (data.ClientsSpawner == null) {
            float popular = _popularityCalculate.GetPopularity();
            data.ClientsSpawner = new(_spawnTime.RandomValue / popular);
        }

        _data = data.ClientsSpawner;
        _spotData = data.SpotManager;
        _criticData = data.CriticSpawner;

        int spotIndex = 0;
        foreach (var spotData in _spotData.Spots) {
            if (!spotData.HaveClients || spotData.ContainsGrayMan()) {
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

        LateStart();
    }

    public void PutClient(Client client) {
        _data.TryRemoveLeavingClient(client.Data);
        _pool.PutObject(client);
    }

    private ClientsHolder SpawnGroupOfClients(CafeSpot spot) {
        if (!spot.TryGetComponent(out ClientsHolder table))
            throw new ArgumentNullException("Spot doesn't have the required class ClientGroupHolder");

        table.SetTutorialState(_tutorialManager.IsWork);
        for (int i = 0; i < spot.SeatsCount; i++) {
            Client client = _pool.GetObject();
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
