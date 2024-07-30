using UnityEngine;

public abstract class PestsPool : Pool<Pest> {
    [Header("Borders")]
    [SerializeField] private Transform _leftDown;
    [SerializeField] private Transform _rightUp;
    protected int _lastId;

    public Pest GetObject(int id) {
        _lastId = id;
        Pest pest = base.GetObject();

        pest.ChangeState(true);
        pest.Randomize(_leftDown.position, _rightUp.position, id);
        return pest;
    }

    public override Pest GetObject() {
        _lastId = -1;
        Pest pest = base.GetObject();

        pest.ChangeState(true);
        pest.Randomize(_leftDown.position, _rightUp.position, _lastId);
        return pest;
    }

    public override void PutObject(Pest pest) {
        base.PutObject(pest);
        pest.ChangeState(false);
    }
}
