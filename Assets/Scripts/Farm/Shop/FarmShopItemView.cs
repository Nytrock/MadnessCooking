using UnityEngine;

public class FarmShopItemView : BaseChooseShopItemView<BaseUpgrade> {
    [SerializeField] private FarmShopNote[] _upgradeTypeNotes;
    [SerializeField] private FarmShopNoteRenderer _noteRenderer;

    protected override void SetInfo(BaseUpgrade upgrade) {
        base.SetInfo(upgrade);
        foreach (var upgradeNote in _upgradeTypeNotes)
            if (upgradeNote.Type == upgrade.Type)
                _noteRenderer.SetNote(upgradeNote);
    }

    public override void ResetInfo() {
        base.ResetInfo();
        _noteRenderer.ResetNote();
    }
}
