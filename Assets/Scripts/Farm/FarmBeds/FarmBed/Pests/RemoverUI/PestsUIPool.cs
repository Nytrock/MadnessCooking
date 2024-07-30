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

    protected override PestUI CreateObject() {
        return Instantiate(_pestPrefab, _container);
    }

    public override void PutObject(PestUI pest) {
        _pool.Enqueue(pest);
        pest.ResetPest();
    }

    public void SetRemover(PestsRemoverUI pestsRemoverUI) {
        _remover = pestsRemoverUI;
    }
}
