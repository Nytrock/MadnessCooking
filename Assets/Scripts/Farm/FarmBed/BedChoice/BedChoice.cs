using UnityEngine;

[RequireComponent(typeof(FarmBed))]
public class BedChoice : MonoBehaviour
{
    [SerializeField] private BedTypeHolder[] _beds;
    private FarmData _data;
    private SerializableFarmBed _bedData;

    private BedChoiceUI _UI;
    private FarmBed _farmBed;

    public void MouseDown()
    {
        if (_bedData.IsActive)
            _farmBed.MouseDown();
        else 
            _UI.ActivateBedChoice(this);
    }

    private void Awake()
    {
        _farmBed = GetComponent<FarmBed>();
    }

    private void LateStart()
    {
        if (!_bedData.IsActive) {
            _farmBed.enabled = false;
            _bedData.IsActive = false;
        }
    }

    public void ReactivateBedsChoice()
    {
        if (_bedData.BedType.Cost > 0)
            MoneyManager.instance.ChangeMoney(_bedData.BedType.Cost);
        _farmBed.ResetBedType();
        _bedData.IsActive = false;
        _UI.ActivateBedChoice(this);
    }

    public void SetType(BedType bedType)
    {
        MoneyManager.instance.ChangeMoney(-bedType.Cost);
        foreach (var bed in _beds) {
            if (bedType == bed.Type) {
                _bedData.IsActive = true;
                _farmBed.SetBedType(bed);
                _farmBed.enabled = true;
                break;
            }
        }
            
    }

    public void Bind(FarmData data, int bedIndex)
    {
        _data = data;
        _bedData = _data.FarmBeds[bedIndex];

        _farmBed.Bind(data, _bedData);
        LateStart();
    }

    public void Setup(FarmBedSettings settings)
    {
        _UI = settings.BedChoiceUI;
        _farmBed.Setup(settings);
    }
}
