using UnityEngine;

[RequireComponent(typeof(PopularityXpAdder))]
public class ClientsSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PopularityCalculator _popularityCalculate;
    [SerializeField] private CafeOpener _cafeOpener;
    [SerializeField] private CafeSpaceManager _spaceManager;
    [SerializeField] private CafeSpotManager _spotManager;
    [SerializeField] private OrdersManager _ordersManager;
    [SerializeField] private ClientsPool _pool;

    [Header("Время спавна")]
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;

    private float _needTime;
    private float _nowTime;
    private bool _isSpawning = true;
    private bool _isOpen = true;
    private bool _isWaitingCritic;

    private PopularityXpAdder _xpAdder;

    private void Awake()
    {
        _xpAdder = GetComponent<PopularityXpAdder>();
    }

    private void Start()
    {
        _cafeOpener.CafeChanged += ChangeWorkMode;
        _spawnPoint.position += new Vector3(_spaceManager.SpaceCount * _spaceManager.GetSpaceSize(), 0, 0);
        SetNewTime();
    }

    private void Update()
    {
        if (!_isSpawning || !_isOpen)
            return;

        if (_nowTime < _needTime) {
            _nowTime += Time.deltaTime;
        } else {
            Spawn();
            SetNewTime();
        }
    }

    private void Spawn()
    {
        var clientType = GetRandomType();
        var spot = _spotManager.GetRandomSpot(clientType);
        if (spot == null)
            return;

        if (spot.SeatsCount > 1) {
            var table = spot.GetComponent<ClientGroupHolder>();
            for (int i = 0; i < spot.SeatsCount; i++) {
                Client client = _pool.GetObject();
                client.ClientEat += ClientEat;
                table.AddClient(client);
                SetupClient(client, spot, clientType, i);
            }
            table.ClientsLeaved += ClientLeave;
            StartCoroutine(table.SpawnGroupOfClients());
        } else {
            Client client = _pool.GetObject();
            SetupClient(client, spot, clientType, 0);
            client.ClientLeave += ClientLeave;
            client.ClientEat += ClientEat;
            client.StartNewCycle();
        }

        if (!_spotManager.CheckHavingSpots())
            ChangeSpawnMode();
    }

    private void SetNewTime()
    {
        var popular = _popularityCalculate.GetPopularity();
        var minTime = _minSpawnTime / popular;
        var maxTime = _maxSpawnTime / popular;

        _needTime = Random.Range(minTime, maxTime);
        _nowTime = 0;
    }

    private void ChangeSpawnMode()
    {
        _isSpawning = !_isSpawning;
        SetNewTime();
    }

    private void ChangeWorkMode()
    {
        _isOpen = !_isOpen;
        SetNewTime();
    }

    private ClientType GetRandomType()
    {
        if (_isWaitingCritic)
            return ClientType.Critic;

        _popularityCalculate.GetClientsNumberChances(out int singleChance, out int doubleChance, out int tripleChance, out int quarterChance);
        var number = Random.Range(1, 1001);
        if (number <= singleChance) {
            number = Random.Range(1, 10001);
            if (number == 1)
                return ClientType.GrayMan;
            else if (number <= 50)
                return ClientType.Rich;
            return ClientType.Standard;

        } else if (number <= doubleChance) {
            return ClientType.Double;
        } else if (number <= tripleChance) {
            return ClientType.Triple;
        }
        return ClientType.Quarter;
    }

    private void ClientLeave(Client client)
    {
        _isSpawning = true;
        _spotManager.ReturnSpot(client.Spot.Index);
        if (!client.IsEat)
            _xpAdder.RemoveXp(client.ClientType);

        client.ClientLeave -= ClientLeave;
        client.ClientEat -= ClientEat;
    }

    private void ClientEat(Client client)
    {
        _xpAdder.AddXp(client.ClientType);
    }

    private void SetupClient(Client client, CafeSpot spot, ClientType clientType, int spotIndex)
    {
        var clientSettings = new ClientSettings(_spawnPoint,
                                                spot.GetTarget(spotIndex),
                                                clientType,
                                                spot,
                                                spotIndex,
                                                _popularityCalculate.GetSpaceMultiplier(),
                                                _pool);

        client.Setup(clientSettings);
        _cafeOpener.CafeChanged += client.Leave;
        _ordersManager.SetNewOrder(client, spot);
    }

    public void ChangeCriticWait(bool newValue)
    {
        _isWaitingCritic = newValue;
    }
}
