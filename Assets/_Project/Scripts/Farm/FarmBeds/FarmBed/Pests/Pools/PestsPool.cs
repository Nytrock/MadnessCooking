using UnityEngine;

public abstract class PestsPool : Pool<Pest> {
    [Header("Borders")]
    [SerializeField] private RangeVector _position;
    protected int _lastId;

    public Pest GetObject(int id) {
        _lastId = id;
        Pest pest = base.GetObject();

        pest.ChangeState(true);
        pest.Randomize(_position, id);
        return pest;
    }

    public override Pest GetObject() {
        _lastId = -1;
        Pest pest = base.GetObject();

        pest.ChangeState(true);
        pest.Randomize(_position, _lastId);
        return pest;
    }

    public override void PutObject(Pest pest) {
        base.PutObject(pest);
        pest.ChangeState(false);
    }
}
