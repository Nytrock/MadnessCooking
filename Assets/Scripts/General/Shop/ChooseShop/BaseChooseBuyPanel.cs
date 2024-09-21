using UnityEngine;

public class BaseChooseBuyPanel : BaseBuyPanel {
    [SerializeField] private BuyableItemRendererWithName _itemInfoRenderer;
    [SerializeField] protected GameObject _outline;

    protected virtual void Awake() {
        _outline.SetActive(false);
    }

    public override void SetVisual() {
        _itemInfoRenderer.SetItemInfo(_data.Item);
    }

    public virtual void UpdateSelectedItem(BuyableItem item) {
        _outline.SetActive(item == _data.Item);
    }
}
