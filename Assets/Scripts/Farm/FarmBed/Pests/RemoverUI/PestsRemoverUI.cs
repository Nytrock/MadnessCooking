using UnityEngine;

public class PestsRemoverUI : MonoBehaviour
{
    [SerializeField] PestsBedTypeUI[] _bedTypes;
    [SerializeField] GameObject _panel;
    [SerializeField] private PestsUIPool _pool;
    private PestsGenerator _generator;
    private PestsBedTypeUI _nowBed;

    private void Start()
    {
        _panel.SetActive(false);
        _pool.SetRemover(this);
        foreach (var bedType in _bedTypes)
            bedType.ChangeState(false);
    }

    public void Activate(BedType bedType, PestsGenerator pestsGenerator)
    {
        _generator = pestsGenerator;
        _generator.ChangePause(true);
        _panel.SetActive(true);

        _nowBed = GetBedType(bedType);
        _nowBed.ChangeState(true);

        GeneratePests();
    }

    public void Close()
    {
        _nowBed.ChangeState(false);
        _generator.ChangePause(false);
        _panel.SetActive(false);
        _nowBed = null;
    }

    private PestsBedTypeUI GetBedType(BedType bedType)
    {
        for (int i = 0; i < _bedTypes.Length; i++) {
            if (_bedTypes[i].BedType == bedType)
                return _bedTypes[i];
        }

        return null;
    }

    private void GeneratePests()
    {
        var pestList = _generator.GetList();
        foreach (var pest in pestList) {
            var pestUI = _pool.GetObject(pest);
            pestUI.SetupRemoveButton();
        }
    }

    public void RemovePest(PestUI pestUI)
    {
        _generator.RemovePest(pestUI.Pest);
        _pool.PutObject(pestUI);
    }
}
