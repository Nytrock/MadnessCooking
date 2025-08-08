using UnityEngine;

[RequireComponent(typeof(GrayscaleImage))]
public class BuyPanelSideInfo : HoverTextActivator {
    private GrayscaleImage _grayscaleImage;

    protected override void Awake() {
        base.Awake();
        _grayscaleImage = GetComponent<GrayscaleImage>();
    }

    public void SetData(BuyPanelSideInfoData sideImageData) {
        if (sideImageData == null) {
            _grayscaleImage.SetActive(false);
            return;
        }

        _grayscaleImage.Setup(sideImageData);
        _textToShow = sideImageData.HoverText;
    }
}
