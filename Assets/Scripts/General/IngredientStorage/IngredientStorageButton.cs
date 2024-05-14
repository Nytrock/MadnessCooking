using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientStorageButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _countText;
    private IngredientCount _count;
    private int _countNum;

    public void SetVisual(IngredientCount count)
    {
        _icon.sprite = count.Ingredient.Icon;
        _count = count;
    }

    private void Update()
    {
        if (_count.Count == _countNum)
            return;

        _countNum = _count.Count;
        _countText.text = _countNum.ToString();
    }
}
