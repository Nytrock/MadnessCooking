using UnityEngine;

public class BaseChooseBuyPanel : BaseBuyPanel {
    [SerializeField] private ItemInfoRendererWithDescription _itemInfoRenderer;

    public override void SetVisual() {
        _itemInfoRenderer.SetItemInfo(_data.Item);
    }

    protected override void SetButtonListener() {
        base.SetButtonListener();
        _buyButton.onClick.AddListener(OnChooseItem);
    }

    protected void OnChooseItem() {
        // Change face
    }
}
