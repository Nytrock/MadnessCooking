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
    private PestsRemoverUI _remover;

    private void Awake()
    {
        _pool = new Queue<PestUI>();
    }

    public PestUI GetObject(Pest pest)
    {
        if (_pool.Count == 0)
            _pool.Enqueue(Instantiate(_pestPrefab, _container));

        var pestUI = _pool.Dequeue();
        pestUI.ChangeState(true);
        pestUI.Setup(pest, _leftDown.position, _rightUp.position, _remover);
        return pestUI;
    }

    public void PutObject(PestUI pest)
    {
        _pool.Enqueue(pest);
        pest.ResetPest();
    }

    public void SetRemover(PestsRemoverUI pestsRemoverUI)
    {
        _remover = pestsRemoverUI;
    }
}
