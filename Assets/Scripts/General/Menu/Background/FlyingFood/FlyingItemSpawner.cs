using UnityEngine;

public class FlyingItemSpawner : MonoBehaviour {
    [SerializeField, Min(0)] private float _minSpawnTime;
    [SerializeField, Min(0)] private float _maxSpawnTime;
    [SerializeField] private FlyingItemPool _pool;
    [SerializeField] private FlyingItemMoveController _moveController;
    [SerializeField] private BuyableItem[] _items;

    private float _nowTime;
    private float _needTime;

    private void Start() {
        SetNewTime();
    }

    private void Update() {
        if (_nowTime < _needTime)
            _nowTime += Time.deltaTime;
        else
            Spawn();
    }

    private void SetNewTime() {
        _nowTime = 0;
        _needTime = Random.Range(_minSpawnTime, _maxSpawnTime);
    }

    private void Spawn() {
        BuyableItem item = _items[Random.Range(0, _items.Length)];
        FlyingItem flyingItem = _pool.GetItem(item);
        flyingItem.BottomReached += PutItem;
        _moveController.SetRandomMove(flyingItem);

        SetNewTime();
    }

    private void PutItem(FlyingItem item) {
        item.BottomReached -= PutItem;
        _pool.PutObject(item);
    }
}
