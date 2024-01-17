using UnityEngine;

[RequireComponent(typeof(GroundBed))]
public class BedChoice : MonoBehaviour
{
    [SerializeField] private BedHolder[] _beds;
    private BedChoiceUI _UI;
    private GroundBed _groundBed;
    private bool _isEmpty;

    private void OnMouseDown()
    {
        if (_isEmpty)
            _UI.Activate(this);
    }

    private void Awake()
    {
        _groundBed = GetComponent<GroundBed>();
        HideBeds();
    }

    private void Start()
    {
        if (_groundBed.AcceptableType == IngredientType.None) {
            _groundBed.enabled = false;
            _isEmpty = true;
        }
    }

    private void HideBeds() { 
        foreach (var bed in _beds)
            bed.gameObject.SetActive(false);
    }

    public void SetType(BedType bedType)
    {
        foreach (var bed in _beds)
            if (bedType == bed.Type) {
                _groundBed.enabled = true;
                _isEmpty = false;
                _groundBed.SetBedType(bed);
                break;
            }
    }

    public void SetUI(BedChoiceUI ui)
    {
        _UI = ui;
    }
}
