using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class BaseChooseBuyPanel : BaseBuyPanel {
    [SerializeField] private BuyableItemRendererWithName _itemInfoRenderer;
    private Outline _outline;

    protected void Awake() {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    public override void SetVisual() {
        _itemInfoRenderer.SetItemInfo(_data.Item);
    }

    protected override void SetButtonListener() {
        base.SetButtonListener();
        _buyButton.onClick.AddListener(OnChooseItem);
    }

    protected void OnChooseItem() {
        _outline.enabled = !_outline.enabled;
    }
}
