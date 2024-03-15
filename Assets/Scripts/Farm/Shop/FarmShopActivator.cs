using UnityEngine;

public class FarmShopActivator : UIActivator
{
    [SerializeField] private FarmShop _shop;
    [SerializeField] private Transform _farm;
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private GameObject _mainUI;
    private bool _isActive;

    protected override void Press()
    {
        ChangeShopState();
    }

    public void ChangeShopState()
    {
        _isActive = !_isActive;
        _mainUI.SetActive(!_isActive);
        _shop.ChangeShopState(_isActive);
        if (_isActive)
            _locationManager.ChangeLocation(_shop.transform);
        else
            _locationManager.ChangeLocation(_farm);
    }
}
