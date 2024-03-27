using UnityEngine;

public class BarnActivator : UIActivator
{
    [SerializeField] private Transform _barn;
    [SerializeField] private Transform _farm;
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private GameObject _mainUI;
    [SerializeField, Min(0)] private float _fatigueCoef;
    private bool _isOpen;

    protected override void Press()
    {
        ChangeBarnState();
    }

    private void ChangeBarnState()
    {
        _isOpen = !_isOpen;
        _mainUI.SetActive(!_isOpen);
        if (_isOpen) {
            _locationManager.ChangeLocation(_barn);
            FatigueManager.instance.ChangeFatigue(_fatigueCoef);
        } else {
            _locationManager.ChangeLocation(_farm);
        }
    }
}
