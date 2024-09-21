using UnityEngine;

public class FarmShopItemView : BaseChooseShopItemView<BaseUpgrade> {
    [SerializeField] private GameObject _panel;

    protected override void SetInfo() {
        base.SetInfo();
        _panel.SetActive(true);
    }

    public override void ResetInfo() {
        base.ResetInfo();
        _panel.SetActive(false);
    }
}
