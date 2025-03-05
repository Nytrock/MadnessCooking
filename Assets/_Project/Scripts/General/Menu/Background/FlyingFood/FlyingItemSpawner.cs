using UnityEngine;

public class FlyingItemSpawner : MonoBehaviour {
    [SerializeField] private RangeFloat _spawnTime;
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
        _needTime = _spawnTime.RandomValue;
    }

    private void Spawn() {
        BuyableItem item = _items.GetRandom();
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
