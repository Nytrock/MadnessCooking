using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientStorageUI<T> : MonoBehaviour where T: ISaveable
{
    [SerializeField] protected IngredientStorage<T> _storage;
    [SerializeField] protected GameObject _panel;
    [SerializeField] protected IngredientStorageButtonPool _buttonPool;
    [SerializeField] private TextMeshProUGUI _sizeRenderer;
    protected List<IngredientStorageButton> _buttons = new();
    private int _maxSize;

    protected virtual void Awake()
    {
        _storage.IngredientAdded += AddButton;
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

    private void AddButton(IngredientCount count)
    {
        var button = _buttonPool.GetObject(count);
        _buttons.Add(button);
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

        var nowSize = _maxSize - _storage.LeftSpace;
        _sizeRenderer.text = $"{nowSize}/{_maxSize}";
    }
}
