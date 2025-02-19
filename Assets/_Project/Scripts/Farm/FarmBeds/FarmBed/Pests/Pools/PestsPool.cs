using UnityEngine;

public abstract class PestsPool : Pool<Pest> {
    [Header("Borders")]
    [SerializeField] protected RangeVector _localPosition;
    [SerializeField] protected RangeVector _globalPosition;
    protected int _prefabIndex;

    public Pest GetObject(int id) {
        _prefabIndex = id;
        Pest pest = base.GetObject();

        pest.ChangeState(true);
        pest.Randomize(_localPosition, _globalPosition, id);
        return pest;
    }

    public override Pest GetObject() {
        _prefabIndex = -1;
        Pest pest = base.GetObject();

        pest.ChangeState(true);
        pest.Randomize(_localPosition, _globalPosition, _prefabIndex);
        return pest;
    }

    public override void PutObject(Pest pest) {
        pest.ChangeState(false);
        base.PutObject(pest);
    }

    public void RemovePest(Pest pest) {
        pest.Remove();
        PutObject(pest);
    }
}
