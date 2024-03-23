using UnityEngine;
using UnityEngine.UI;

public class FarmCarUI : IngredientStorageUI, IUpgradeable
{
    [SerializeField] private Button _sendButton;

    protected override void Awake()
    {
        var car = _storage.GetComponent<FarmCar>();
        car.CarReturned += CarReturn; 
        base.Awake();
    }

    public void CarLeave()
    {
        foreach(var button in _buttons)
            _buttonPool.PutObject(button);
        _buttons.Clear();
        UpdateSizeRenderer();
        _panel.SetActive(false);
        _sendButton.interactable = false;
    }

    private void CarReturn()
    {
        _sendButton.interactable = true;
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {

    }
}
