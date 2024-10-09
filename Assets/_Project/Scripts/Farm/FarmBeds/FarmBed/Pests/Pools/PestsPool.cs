using UnityEngine;

public abstract class PestsPool : Pool<Pest> {
    [Header("Borders")]
    [SerializeField] private RangeVector _localPosition;
    [SerializeField] private RangeVector _globalPosition;
    protected int _lastId;

    public Pest GetObject(int id) {
        _lastId = id;
        Pest pest = base.GetObject();

        pest.ChangeState(true);
        pest.Randomize(_localPosition, _globalPosition, id);
        return pest;
    }

    public override Pest GetObject() {
        _lastId = -1;
        Pest pest = base.GetObject();

        pest.ChangeState(true);
        pest.Randomize(_localPosition, _globalPosition, _lastId);
        return pest;
    }

    public override void PutObject(Pest pest) {
        base.PutObject(pest);
        pest.ChangeState(false);
    }
}
