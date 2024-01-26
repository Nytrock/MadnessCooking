using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarUI : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private GameObject _panel;
    [SerializeField] private CarButtonPool _buttonPool;
    [SerializeField] private TextMeshProUGUI _sizeShower;
    [SerializeField] private Button _sendButton;
    private readonly List<CarButton> _buttons = new();

    private void Start()
    {
        _car.CountChanged += SetButton;
        _car.CarReturned += CarReturn; 

        _sizeShower.text = $"0/{_car.MaxSize}";
        _panel.SetActive(false);
    }

    public void ChangePanelState()
    {
        _panel.SetActive(!_panel.activeSelf);
    }

    public void SetButton(int index)
    {
        var count = _car.GetIngredient(index);
        if (index == _buttons.Count) {
            var button = _buttonPool.GetObject();
            button.SetVisual(count);
            _buttons.Add(button);
        } else {
            _buttons[index].SetCount(count.Count);
        }

        UpdateSizeShower();
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

    private void UpdateSizeShower()
    {
        var maxSize = _car.MaxSize;
        var nowSize = maxSize - _car.GetSpace();
        _sizeShower.text = $"{nowSize}/{maxSize}";
    }
}
