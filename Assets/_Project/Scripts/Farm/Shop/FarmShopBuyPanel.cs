using UnityEngine;
using UnityEngine.UI;

public class FarmShopBuyPanel : BaseChooseBuyPanel {
    [SerializeField] private Image[] _outlines;

    public override void SetVisual() {
        base.SetVisual();
        foreach (var outline in _outlines)
            outline.sprite = _data.Item.Icon;
    }

    public override void SetSideInfo() { }
    public override void SetSideInfoHoverPanel(HoverTextPanel hoverPanel) { }
}
