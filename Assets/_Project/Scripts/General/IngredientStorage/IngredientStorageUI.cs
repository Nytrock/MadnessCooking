using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientStorageUI : MonoBehaviour, IActivable {
    [SerializeField] protected IngredientStorage _storage;
    [SerializeField] protected GameObject _panel;
    [SerializeField] protected IngredientCountButtonPool _buttonPool;
    [SerializeField] private TextMeshProUGUI _sizeText;
    protected List<IngredientCountButton> _buttons = new();

    public event Action<bool> StateChanged;

    protected virtual void Awake() {
        _storage.IngredientAdded += AddIngredientCount;
        _storage.IngredientRemoved += RemoveIngredient;
    }

    protected virtual void Start() {
        _panel.SetActive(false);
    }

    public void ChangeState(bool newState) {
        _panel.SetActive(newState);
        StateChanged?.Invoke(newState);
    }

    private void AddIngredientCount(IngredientCount count) {
        IngredientCountButton button = _buttonPool.GetObject(count);
        button.IngredientCount.CountChanged += delegate { UpdateSizeRenderer(); };
        _buttons.Add(button);
    }

    private void RemoveIngredientCount(IngredientCountButton button) {
        button.IngredientCount.CountChanged -= delegate { UpdateSizeRenderer(); };
        _buttons.Remove(button);
        _buttonPool.PutObject(button);
    }

    private void RemoveIngredient(Ingredient ingredient) {
        foreach (var button in _buttons) {
            if (button.IngredientCount.Item == ingredient) {
                RemoveIngredientCount(button);
                break;
            }
        }
    }

    protected void UpdateSizeRenderer() {
        if (_sizeText == null)
            return;

        if (_storage.Data.MaxSpace == -1) {
            _sizeText.text = "";
            return;
        }

        _sizeText.text = $"{_storage.Data.NowSpace}/{_storage.Data.MaxSpace}";
    }
}
