using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(GroundBed))]
public class BedChoice : MonoBehaviour
{
    [SerializeField] private BedTypeHolder[] _beds;
    private BedChoiceUI _UI;
    private GroundBed _groundBed;
    private bool _isEmpty;

    public void MouseDown()
    {
        if (_isEmpty)
            _UI.Activate(this);
        else
            _groundBed.MouseDown();
    }

    private void Awake()
    {
        _groundBed = GetComponent<GroundBed>();
        HideBeds();
    }

    private void Start()
    {
        if (_groundBed.BedType == null) {
            _groundBed.enabled = false;
            _isEmpty = true;
        }
    }

    public void ReactivateBedsChoice()
    {
        if (_groundBed.BedType.Cost > 0)
            MoneyManager.instance.ChangeMoney(_groundBed.BedType.Cost);
        _groundBed.ResetBedType();
        _isEmpty = true;
        _UI.Activate(this);
    }

    private void HideBeds() { 
        foreach (var bed in _beds)
            bed.gameObject.SetActive(false);
    }

    public void SetType(BedType bedType)
    {
        MoneyManager.instance.ChangeMoney(-bedType.Cost);
        foreach (var bed in _beds)
            if (bedType == bed.Type) {
                _isEmpty = false;
                _groundBed.SetBedType(bed);
                _groundBed.enabled = true;
                break;
            }
    }

    public void Setup(GroundBedSettings settings)
    {
        _UI = settings.BedChoiceUI;
    }
}
