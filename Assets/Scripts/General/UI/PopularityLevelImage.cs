using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class PopularityLevelImage : MonoBehaviour {
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private PopularityLevelImageType _type;
    private Image _image;

    private void Awake() {
        GetImage();
    }

    public void SetSprite(int levelNum) {
        GetImage();

        if (_type == PopularityLevelImageType.PerStage)
            _image.sprite = _sprites[levelNum / 5];
        else if (_type == PopularityLevelImageType.PerLevel)
            _image.sprite = _sprites[levelNum % 5];
    }

    private void GetImage() {
        if (_image != null)
            return;

        _image = GetComponent<Image>();
    }

    private enum PopularityLevelImageType {
        None,
        PerStage,
        PerLevel,
    }
}
