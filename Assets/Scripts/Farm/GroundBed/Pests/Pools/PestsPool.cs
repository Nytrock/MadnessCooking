using System.Collections.Generic;
using UnityEngine;

public abstract class PestsPool : MonoBehaviour
{
    [SerializeField] protected Transform _container;
    private Queue<Pest> _pool;

    private void Awake()
    {
        _pool = new Queue<Pest>();
    }

    public Pest GetObject()
    {
        if (_pool.Count == 0)
            _pool.Enqueue(SpawnPest());

        var pest = _pool.Dequeue();
        pest.ChangeState(true);
        pest.Randomize();
        return pest;
    }

    public void PutObject(Pest pest)
    {
        _pool.Enqueue(pest);
        pest.ChangeState(false);
    }

    protected abstract Pest SpawnPest();
}
