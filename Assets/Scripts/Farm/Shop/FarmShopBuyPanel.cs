using UnityEngine.UI;

public class FarmShopBuyPanel : BaseChooseBuyPanel {
    private Image _outlineImage;

    protected override void Awake() {
        base.Awake();
        _outlineImage = _outline.GetComponent<Image>();
    }

    public override void SetVisual() {
        base.SetVisual();
        _outlineImage.sprite = _data.Item.Icon;
    }
}
