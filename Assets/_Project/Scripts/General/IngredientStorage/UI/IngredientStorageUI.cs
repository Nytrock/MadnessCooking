using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientStorageUI<TData> : MonoBehaviour, IActivable
    where TData : ISaveable {

    [SerializeField] protected SaveableIngredientStorage<TData> _storage;
    [SerializeField] protected GameObject _panel;
    [SerializeField] protected IngredientStorageButtonPool _buttonPool;
    [SerializeField] private TextMeshProUGUI _sizeText;
    protected List<IngredientStorageButton> _buttons = new();

    protected virtual void Awake() {
        _storage.IngredientCountAdded += CheckChangedIngredient;
    }

    protected virtual void Start() {
        _panel.SetActive(false);
    }

    protected virtual void Update() {
        UpdateSizeRenderer();
    }

    public void ChangeState() {
        _panel.SetActive(!_panel.activeSelf);
    }

    public void ChangeState(bool newState) {
        _panel.SetActive(newState);
    }

    private void CheckChangedIngredient(BuyableItemCount<Ingredient> count) {
        IngredientStorageButton button = _buttonPool.GetObject(count);
        _buttons.Add(button);
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
