using UnityEngine.Events;

public class BuyPanelData {
    private BuyableObject _item;
    private bool _isBuyable;
    private UnityAction _panelAction;
    private GrayscaleImageData _sideImageData;

    public BuyableObject Item => _item;
    public bool IsBuyable => _isBuyable;
    public UnityAction PanelAction => _panelAction;
    public GrayscaleImageData SideImageData => _sideImageData;

    public BuyPanelData(BuyableObject item, bool isBuyable, UnityAction action, GrayscaleImageData sideImageData) {
        _item = item;
        _isBuyable = isBuyable;
        _sideImageData = sideImageData;
        _panelAction = action;
    }
}
