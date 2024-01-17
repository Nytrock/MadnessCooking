using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BedChoiceDescrUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _descr;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private BedTypeIngredientsShower _ingredientsShower;
    private bool _isActive;

    public void UpdateDescription(BedType bedType)
    {
        _ingredientsShower.ShowIngredients(bedType);
        _icon.sprite = bedType.BedSprite;
        _name.text = bedType.Name;
        _descr.text = bedType.Description;
        if (bedType.Cost > 0)
            _cost.text = bedType.Cost.ToString();
        else
            _cost.text = "Free";
    }

    public void ChangeActive()
    {
        _isActive = !_isActive;
        gameObject.SetActive(_isActive);
    }

    public void Disable()
    {
        _isActive = false;
        gameObject.SetActive(false);
    }
}
