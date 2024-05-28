using System;
using UnityEngine;

[RequireComponent(typeof(FarmBed))]
public class BedChoice : MonoBehaviour {
    [SerializeField] private BedTypeHolder[] _beds;
    private FarmData _data;
    private FarmBedData _bedData;

    private BedChoiceUI _UI;
    private FarmBed _farmBed;

    public void MouseDown() {
        if (_bedData.IsActive)
            _farmBed.MouseDown();
        else
            _UI.ActivateBedChoice(this);
    }

    private void Awake() {
        _farmBed = GetComponent<FarmBed>();
    }

    private void LateStart() {
        if (!_bedData.IsActive) {
            _farmBed.enabled = false;
            _bedData.IsActive = false;
        }
    }

    public void SetType(BedType bedType) {
        MoneyManager.Instance.ChangeMoney(-bedType.Price);
        BedTypeHolder bed = FindBedHolder(bedType);

        _bedData.IsActive = true;
        _farmBed.SetBedType(bed);
        _farmBed.enabled = true;
    }

    public void Bind(FarmData data, int bedIndex) {
        _data = data;
        _bedData = _data.FarmBeds[bedIndex];

        _farmBed.Bind(data, _bedData, FindBedHolder(_bedData.BedType));
        LateStart();
    }

    public void Setup(FarmBedSettings settings) {
        _UI = settings.BedChoiceUI;
        _farmBed.Setup(settings);
    }

    private BedTypeHolder FindBedHolder(BedType bedType) {
        if (bedType == null)
            return null;

        foreach (var bed in _beds)
            if (bedType == bed.Type)
                return bed;

        throw new ArgumentNullException("There's no bed holder with such bed Type");
    }
}
