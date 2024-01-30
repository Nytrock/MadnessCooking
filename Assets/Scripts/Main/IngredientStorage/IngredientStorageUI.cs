using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientStorageUI : MonoBehaviour
{
    [SerializeField] protected IngredientStorage _storage;
    [SerializeField] protected GameObject _panel;
    [SerializeField] protected IngredientStorageButtonPool _buttonPool;
    [SerializeField] private TextMeshProUGUI _sizeShower;
    protected List<IngredientStorageButton> _buttons = new();

    protected virtual void Start()
    {
        _storage.CountChanged += SetButton;
        if (_storage.MaxSize != -1)
            _sizeShower.text = $"0/{_storage.MaxSize}";
        _panel.SetActive(false);
    }

    public void ChangePanelState()
    {
        _panel.SetActive(!_panel.activeSelf);
    }

    public void SetButton(int index)
    {
        var count = _storage.GetIngredient(index);
        if (index == _buttons.Count) {
            var button = _buttonPool.GetObject();
            button.SetVisual(count);
            _buttons.Add(button);
        } else {
            _buttons[index].SetCount(count.Count);
        }

        UpdateSizeShower();
    }

    protected void UpdateSizeShower()
    {
        if (_storage.MaxSize == -1)
            return;

        var maxSize = _storage.MaxSize;
        var nowSize = maxSize - _storage.GetSpace();
        _sizeShower.text = $"{nowSize}/{maxSize}";
    }
}
