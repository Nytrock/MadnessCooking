using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBuyPanel : MonoBehaviour {
    [SerializeField] protected BuyPanelSideInfo _sideInfo;
    [SerializeField] protected Button _buyButton;
    protected BuyPanelData _data;

    public virtual void Destroy() {
        Destroy(gameObject);
    }

    public virtual void Setup(BuyPanelData data) {
        _data = data;

        SetVisual();
        SetButtonListener();
        SetSideInfo();
    }

    public virtual void SetSideInfo() {
        _sideInfo.SetData(_data.SideImageData);
    }

    public virtual void SetSideInfoHoverPanel(HoverTextPanel hoverPanel) {
        _sideInfo.SetHoverPanel(hoverPanel);
    }

    protected virtual void SetButtonListener() {
        _buyButton.OverrideAllListeners(_data.PanelAction);
    }

    public abstract void SetVisual();
}
