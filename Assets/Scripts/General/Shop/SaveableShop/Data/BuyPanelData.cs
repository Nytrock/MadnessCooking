using UnityEngine.Events;

public class BuyPanelData {
    private readonly BuyableItem _item;
    private readonly bool _isBuyable;
    private readonly UnityAction _panelAction;
    private readonly GrayscaleImageData _sideImageData;

    public BuyableItem Item => _item;
    public bool IsBuyable => _isBuyable;
    public UnityAction PanelAction => _panelAction;
    public GrayscaleImageData SideImageData => _sideImageData;

    public BuyPanelData(BuyableItem item, bool isBuyable, UnityAction action, GrayscaleImageData sideImageData) {
        _item = item;
        _isBuyable = isBuyable;
        _sideImageData = sideImageData;
        _panelAction = action;
    }
}
