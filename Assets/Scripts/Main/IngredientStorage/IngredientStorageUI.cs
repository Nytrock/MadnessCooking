using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientStorageUI : MonoBehaviour
{
    [SerializeField] protected IngredientStorage _storage;
    [SerializeField] protected GameObject _panel;
    [SerializeField] protected IngredientStorageButtonPool _buttonPool;
    [SerializeField] private TextMeshProUGUI _sizeRenderer;
    protected List<IngredientStorageButton> _buttons = new();
    private int _maxSize;

    protected virtual void Awake()
    {
        _storage.ElementCountChanged += SetButton;
        _storage.MaxSizeChanged += UpdateMaxValue;
    }

    private void Start()
    {
        _panel.SetActive(false);
    }

    public void ChangePanelState()
    {
        _panel.SetActive(!_panel.activeSelf);
    }

    public void SetButton(int index)
    {
        var count = _storage.GetIngredientByIndex(index);
        if (index == _buttons.Count) {
            var button = _buttonPool.GetObject(count);
            _buttons.Add(button);
        } else {
            _buttons[index].SetCount(count.Count);
        }

        UpdateSizeRenderer();
    }

    protected void UpdateMaxValue(int newMax)
    {
        _maxSize = newMax;
        if (_sizeRenderer != null && _maxSize == -1)
            _sizeRenderer.text = "";
        UpdateSizeRenderer();
    }

    protected void UpdateSizeRenderer()
    {
        if (_maxSize == -1 || _sizeRenderer == null)
            return;

        var nowSize = _maxSize - _storage.GetSpace();
        _sizeRenderer.text = $"{nowSize}/{_maxSize}";
    }
}
