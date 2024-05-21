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

    protected virtual void Awake()
    {
        _storage.IngredientAdded += AddButton;
    }

    private void Start()
    {
        _panel.SetActive(false);
    }

    private void Update()
    {
        UpdateSizeRenderer();
    }

    public void ChangePanelState()
    {
        _panel.SetActive(!_panel.activeSelf);
    }

    private void AddButton(IngredientCount count)
    {
        var button = _buttonPool.GetObject(count);
        _buttons.Add(button);
    }

    protected void UpdateSizeRenderer()
    {
        if (_sizeRenderer == null)
            return;

        if (_storage.Data.MaxSpace == -1) {
            _sizeRenderer.text = "";
            return;
        }

        _sizeRenderer.text = $"{_storage.Data.NowSpace}/{_storage.Data.MaxSpace}";
    }
}
