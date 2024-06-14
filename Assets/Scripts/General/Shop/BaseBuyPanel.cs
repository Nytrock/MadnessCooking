using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBuyPanel : MonoBehaviour {
    [SerializeField] protected GrayscaleImageRenderer _sideInfo;
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
        if (_data.SideImageData == null) {
            _sideInfo.SetActive(false);
            return;
        }

        _sideInfo.SetActive(true);
        _sideInfo.Setup(_data.SideImageData);
    }

    protected virtual void SetButtonListener() {
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.onClick.AddListener(_data.PanelAction);
    }

    public abstract void SetVisual();
}
