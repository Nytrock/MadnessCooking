using UnityEngine;

public class PestsRemoverUI : MonoBehaviour
{
    [SerializeField] PestsBedTypeUI[] _bedTypes;
    [SerializeField] GameObject _panel;
    [SerializeField] private PestsUIPool _pool;
    private PestsGenerator _generator;
    private PestsBedTypeUI _nowBed;
    private int _pestCount;

    private void Start()
    {
        _panel.SetActive(false);
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
        _pestCount = pestList.Count;
        foreach (var pest in pestList) {
            var pestUI = _pool.GetObject(pest);
            pestUI.ButtonRemove.onClick.AddListener(delegate { RemovePest(pestUI); });
        }
    }

    private void RemovePest(PestUI pestUI)
    {
        _generator.RemovePest(pestUI.Pest);
        _pestCount--;
        _pool.PutObject(pestUI);
    }
}
