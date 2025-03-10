using UnityEngine;

public class ItemsListItemDescription : MonoBehaviour {
    [SerializeField] private BuyableItemRendererWithDescription _description;
    private ItemsListItemRenderer _nowRenderer;

    private void Awake() {
        ChangeState(false);
    }

    public void ChangeState(bool newState) {
        gameObject.SetActive(newState);

        if (!newState && _nowRenderer != null) {
            _nowRenderer.ChangeSelectionState(false);
            _nowRenderer = null;
        }
    }

    public void SelectItem(ItemsListItemRenderer itemRenderer) {
        if (itemRenderer == _nowRenderer) {
            ChangeState(false);
            return;
        }

        if (_nowRenderer != null)
            _nowRenderer.ChangeSelectionState(false);
        _nowRenderer = itemRenderer;
        _nowRenderer.ChangeSelectionState(true);
        UpdateDescription();
    }

    public void UpdateDescription() {
        if (_nowRenderer == null)
            return;

        if (_nowRenderer.IsUnlocked)
            _description.SetItemInfo(_nowRenderer.Item);
        else
            _description.SetHiddenItemInfo(_nowRenderer.Item);
        ChangeState(true);
    }
}