using UnityEngine;

public class ClientsSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PopularityManager _popularityManager;
    [SerializeField] private CafeOpener _cafeOpener;
    [SerializeField] private CafeManager _cafeManager;
    [SerializeField] private ClientsPool _pool;

    [Header("Время спавна")]
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;

    private float _needTime;
    private float _nowTime;
    private bool _isSpawning = true;

    private void Start()
    {
        _cafeOpener.CafeChanged += ChangeWorkMode;
        _spawnPoint.position += new Vector3(_cafeManager.SpaceCount * _cafeManager.GetSpaceSize(), 0, 0);
        SetNewTime();
    }

    private void Update()
    {
        if (!_isSpawning)
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
        if (!_cafeManager.CheckHavingSpots()) {
            ChangeWorkMode();
            return;
        }

        var clientType = GetRandomType();
        var spot = _cafeManager.GetRandomSpot(clientType);
        if (spot == null)
            return;

        if (spot.SeatsCount > 1) {

        } else {
            Debug.Log("Spawn");
            Client client = _pool.GetObject();
            client.transform.position = _spawnPoint.position;
            _cafeOpener.CafeChanged += client.Leave;
            client.SetType(clientType);
            client.SetTargets(spot.GetTarget(), _spawnPoint);
        }
    }

    private void SetNewTime()
    {
        var popular = _popularityManager.GetPopularity();
        _needTime = Random.Range(_minSpawnTime / popular, _maxSpawnTime / popular);
        _nowTime = 0;
    }

    private void ChangeWorkMode()
    {
        _isSpawning = !_isSpawning;
        SetNewTime();
    }

    private ClientType GetRandomType()
    {
        _popularityManager.GetClientsNumberChances(out int singleChance, out int doubleChance, out int tripleChance, out int quarterChance);
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
}
