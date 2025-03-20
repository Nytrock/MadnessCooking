using UnityEngine;

public class ItemsListItemDescription : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private BuyableItemRendererWithDescription _description;
    private ItemsListItemRenderer _nowRenderer;

    public void ChangeState(bool newState) {
        _panel.SetActive(newState);

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