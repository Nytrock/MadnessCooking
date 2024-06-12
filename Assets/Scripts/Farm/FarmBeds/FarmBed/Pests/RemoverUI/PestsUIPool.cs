using UnityEngine;

public class PestsUIPool : Pool<PestUI> {
    [SerializeField] private PestUI _pestPrefab;

    [Header("Borders")]
    [SerializeField] private Transform _leftDown;
    [SerializeField] private Transform _rightUp;
    private PestsRemoverUI _remover;

    public PestUI GetObject(Pest pest) {
        PestUI pestUI = GetObject();
        pestUI.ChangeState(true);
        pestUI.Setup(pest, _leftDown.position, _rightUp.position, _remover);
        return pestUI;
    }

    public override PestUI GetObject() {
        if (_pool.Count == 0)
            return Instantiate(_pestPrefab, _container);
        return _pool.Dequeue();
    }

    public override void PutObject(PestUI pest) {
        _pool.Enqueue(pest);
        pest.ResetPest();
    }

    public void SetRemover(PestsRemoverUI pestsRemoverUI) {
        _remover = pestsRemoverUI;
    }
}
