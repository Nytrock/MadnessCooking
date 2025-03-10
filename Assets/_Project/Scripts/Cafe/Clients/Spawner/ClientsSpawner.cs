using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PopularityXpAdder))]
public class ClientsSpawner : MonoBehaviour, IBindable<CafeData>, ITutorialPart {
    [SerializeField] private ClientSpawnPoint _spawnPoint;
    [SerializeField] private GameTimeManager _timeManager;
    [SerializeField] private LocationNotificationManager _notificationManager;
    [SerializeField] private PopularityCalculator _popularityCalculate;
    [SerializeField] private CafeStateChanger _cafeOpener;
    [SerializeField] private ClientsHolderManager _clientsHolderManager;
    [SerializeField] private OrdersManager _ordersManager;
    [SerializeField] private FoodManager _foodManager;
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private ClientsPoolsManager _pool;
    [SerializeField] private RangeFloat _spawnTime;
    [SerializeField] private float _noClientsMultiplier;

    private ClientsSpawnerData _data;
    private CriticSpawnerData _criticData;
    private ClientHolderManagerData _clientHolderData;
    private PopularityXpAdder _xpAdder;

    public event Action PartEnded;

    public Vector2 SpawnPoint => _spawnPoint.Position;

    private void Awake() {
        _xpAdder = GetComponent<PopularityXpAdder>();
        _cafeOpener.CafeChanged += delegate { SetNewSpawnTime(); };
    }

    public void LateStart() {
        GenerateClientsFromData();
        SetNewSpawnTime();

        _timeManager.DaytimeChanged += CheckDaytime;
    }

    private void CheckDaytime(Daytime daytime) {
        if (daytime == Daytime.Morning || daytime == Daytime.Night)
            SetNewSpawnTime();
    }

    private void Update() {
        if (!_cafeOpener.IsOpened || _tutorialManager.IsWork)
            return;

        _data.AddTime();
        if (_data.NowSpawnTime > _data.NeedSpawnTime)
            Spawn();
    }

    private void Spawn() {
        ClientCount clientCount = GetRandomCount();
        ClientsHolder holder = _clientsHolderManager.TakeRandomHolder(clientCount);
        if (holder == null) {
            SetNewSpawnTime();
            return;
        }

        ClientType clientType = GetRandomType(clientCount);
        for (int i = 0; i < holder.ClientsCount; i++) {
            Order order = new(_foodManager.GetFoodForOrder(), holder.Index + 1);
            ClientData newClient = new(_spawnPoint.Position, clientType, order);
            holder.Data.SetClient(i, newClient);
        }

        SpawnGroupOfClients(holder);
        SetNewSpawnTime();
    }

    private void GenerateClientsFromData() {
        int holderIndex = 0;
        foreach (var clientsHolderData in _clientHolderData.ClientHolders) {
            ClientsHolder clientsHolder = _clientsHolderManager.TakeHolder(holderIndex);
            clientsHolder.SetData(clientsHolderData);

            if (!clientsHolderData.HaveClients) {
                holderIndex++;
                continue;
            }

            if (clientsHolderData.ContainsGrayMan()) {
                clientsHolderData.EndVisit();
                holderIndex++;
                continue;
            }

            SpawnGroupOfClients(clientsHolder);

            if (clientsHolderData.GroupState == GroupClientState.Wait)
                clientsHolder.CheckWait();
            holderIndex++;
        }

        foreach (var clientData in _data.LeavingClients) {
            Client client = _pool.GetClient(clientData);
            client.transform.position = clientData.Position;
            client.Setup(new ClientSettings(clientData, null, null));
        }
    }

    private void SetNewSpawnTime() {
        float popularity = _popularityCalculate.GetPopularity();
        float spawnTime = _spawnTime.RandomValue / popularity;

        if (_data.NowClientsCount == 0)
            spawnTime *= _noClientsMultiplier;

        _data.SetSpawnTime(spawnTime);
    }

    private ClientCount GetRandomCount() {
        if (_criticData.IsCriticCanSpawn || _tutorialManager.IsWork)
            return ClientCount.One;

        return _popularityCalculate.GetClientCount();
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

    private void SpawnGroupOfClients(ClientsHolder holder) {
        holder.SetTutorialState(_tutorialManager.IsWork);

        for (int i = 0; i < holder.ClientsCount; i++) {
            _data.AddNowClient();
            ClientData clientData = holder.Data.GetClient(i);
            Client client = _pool.GetClient(clientData);
            CafeSeat seat = holder.GetSeat(i);

            client.ClientEat += ClientEat;
            client.ClientRejected += ClientRejected;
            holder.AddClient(client);
            SetupClient(client, clientData, holder, seat);
        }

        if (_tutorialManager.IsWork)
            holder.WaitStarted += _tutorialManager.NextTutorialPart;
        holder.WaitStarted += CreateNotification;
        holder.ClientsLeaved += ClientsLeave;
        StartCoroutine(holder.SpawnGroupOfClients(_spawnPoint));
    }

    private void SetupClient(Client client, ClientData clientData, ClientsHolder holder, CafeSeat seat) {
        ClientSettings clientSettings = new(clientData, holder, seat);
        client.Setup(clientSettings);
        _ordersManager.AddNewClient(client);
        client.EndSetup();
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

    private void ClientsLeave(ClientsHolder holder) {
        _clientsHolderManager.ReturnHolder(holder.Index);
        holder.ClientsLeaved -= ClientsLeave;

        for (int i = 0; i < holder.ClientsCount; i++)
            _data.AddLeavingClient(holder.Data.GetClient(i));
    }

    public void PutClient(Client client) {
        _data.TryRemoveLeavingClient(client.Data);
        _pool.PutObject(client);
    }

    private void CreateNotification() {
        _notificationManager.CreateNotification(Location.Cafe);
    }

    public void StartTutorialPart() {
        Spawn();
        PartEnded?.Invoke();
    }

    public void Bind(CafeData data) {
        data.ClientsSpawner ??= new();
        _data = data.ClientsSpawner;

        _clientHolderData = data.ClientHolderManager;
        _criticData = data.CriticSpawner;
    }
}
