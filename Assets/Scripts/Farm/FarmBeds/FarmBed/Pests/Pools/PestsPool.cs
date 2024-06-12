using UnityEngine;

public abstract class PestsPool : Pool<Pest> {
    [Header("Borders")]
    [SerializeField] private Transform _leftDown;
    [SerializeField] private Transform _rightUp;

    public Pest GetObject(int id) {
        if (_pool.Count == 0)
            _pool.Enqueue(SpawnPest(ref id));

        Pest pest = _pool.Dequeue();
        pest.ChangeState(true);
        pest.Randomize(_leftDown.position, _rightUp.position, id);
        return pest;
    }

    public override Pest GetObject() {
        int id = -1;
        if (_pool.Count == 0)
            _pool.Enqueue(SpawnPest(ref id));
        Pest pest = _pool.Dequeue();

        pest.ChangeState(true);
        pest.Randomize(_leftDown.position, _rightUp.position, id);
        return pest;
    }

    public override void PutObject(Pest pest) {
        _pool.Enqueue(pest);
        pest.ChangeState(false);
    }

    protected abstract Pest SpawnPest(ref int id);
}
