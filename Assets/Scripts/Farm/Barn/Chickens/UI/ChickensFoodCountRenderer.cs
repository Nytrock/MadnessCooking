using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChickensFoodCountRenderer : MonoBehaviour {
    [SerializeField] private BaseUpgrade _food;
    [SerializeField] private BaseUpgrade _infiniteFood;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private Image _icon;
    private Chickens _chickens;

    public void SetChickens(Chickens chickens) {
        _chickens = chickens;
    }

    public void UpdateFoodCount() {
        if (_chickens.Data.IsInfiniteFood) {
            _icon.sprite = _infiniteFood.Icon;
            _countText.text = "";
        } else {
            _icon.sprite = _food.Icon;
            _countText.text = "x" + _chickens.Data.FoodCount.ToString();
        }
    }
}
