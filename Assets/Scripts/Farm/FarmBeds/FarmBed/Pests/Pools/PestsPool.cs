using System.Collections.Generic;
using UnityEngine;

public abstract class PestsPool : MonoBehaviour {
    [SerializeField] protected Transform _container;
    private Queue<Pest> _pool;

    [Header("Borders")]
    [SerializeField] private Transform _leftDown;
    [SerializeField] private Transform _rightUp;

    private void Awake() {
        _pool = new Queue<Pest>();
    }

    public Pest GetObject(int id = -1) {
        if (_pool.Count == 0)
            _pool.Enqueue(SpawnPest(ref id));

        Pest pest = _pool.Dequeue();
        pest.ChangeState(true);
        pest.Randomize(_leftDown.position, _rightUp.position, id);
        return pest;
    }

    public void PutObject(Pest pest) {
        _pool.Enqueue(pest);
        pest.ChangeState(false);
    }

    protected abstract Pest SpawnPest(ref int id);
}
