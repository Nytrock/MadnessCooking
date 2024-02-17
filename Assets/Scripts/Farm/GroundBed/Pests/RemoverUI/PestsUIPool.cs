using System.Collections.Generic;
using UnityEngine;

public class PestsUIPool : MonoBehaviour
{
    [SerializeField] private PestUI _pestPrefab;
    [SerializeField] private Transform _container;

    [Header("Borders")]
    [SerializeField] private Transform _leftDown;
    [SerializeField] private Transform _rightUp;

    private Queue<PestUI> _pool;

    private void Awake()
    {
        _pool = new Queue<PestUI>();
    }

    public PestUI GetObject(Pest originalPest)
    {
        if (_pool.Count == 0)
            _pool.Enqueue(Instantiate(_pestPrefab, _container));

        var pestUI = _pool.Dequeue();
        pestUI.ChangeState(true);
        pestUI.Setup(originalPest, _leftDown, _rightUp);
        return pestUI;
    }

    public void PutObject(PestUI pest)
    {
        _pool.Enqueue(pest);
        pest.ResetPest();
    }
}
