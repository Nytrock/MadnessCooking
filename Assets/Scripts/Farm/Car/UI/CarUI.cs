using UnityEngine;
using UnityEngine.UI;

public class CarUI : IngredientStorageUI
{
    [SerializeField] private Button _sendButton;

    protected override void Start()
    {
        var car = _storage.GetComponent<Car>();
        car.CarReturned += CarReturn; 
        base.Start();
    }

    public void CarLeave()
    {
        foreach(var button in _buttons)
            _buttonPool.PutObject(button);
        _buttons.Clear();
        UpdateSizeShower();
        _panel.SetActive(false);
        _sendButton.interactable = false;
    }

    private void CarReturn()
    {
        _sendButton.interactable = true;
    }
}
