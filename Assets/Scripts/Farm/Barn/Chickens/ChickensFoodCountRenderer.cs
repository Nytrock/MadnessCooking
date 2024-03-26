using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChickensFoodCountRenderer : MonoBehaviour
{
    [SerializeField] private BaseUpgrade _food;
    [SerializeField] private BaseUpgrade _infiniteFood;
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private Image _icon;
    private Chickens _chickens;

    public void SetChickens(Chickens chickens)
    {
        _chickens = chickens;
    }

    public void UpdateFoodCount()
    {
        if (_chickens.IsInfiniteFood) {
            _icon.sprite = _infiniteFood.Icon;
            _count.text = "";
        } else {
            _icon.sprite = _food.Icon;
            _count.text = "x" + _chickens.FoodCount.ToString();
        }
    }
}
