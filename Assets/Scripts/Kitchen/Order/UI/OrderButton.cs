using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _tableCount;

    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _cookingSlider;
    [SerializeField] private GameObject _finishText;

    [SerializeField] private OrderRecipe _recipe;
    [SerializeField] private Button _cookButton;
    private bool _isCooking;
    private Order _order;

    private void Start()
    {
        StartNewCycle();
    }

    public void StartNewCycle()
    {
        _isCooking = false;
        _cookingSlider.SetActive(false);
        _startButton.SetActive(true);
        _finishText.SetActive(false);
        gameObject.SetActive(true);
    }

    public void StartSetup(Order order)
    {
        StartNewCycle();
        _order = order;

        _icon.sprite = _order.Food.FoodSprite;
        _title.text = _order.Food.Name;
        _tableCount.text = _order.TableNumber.ToString();

        var canCook = _recipe.CreateRecipe(_order);
        _cookButton.interactable = canCook;
    }

    public void UpdateRecipe()
    {
        if (_order == null) return;

        var canCook = _recipe.CreateRecipe(_order);
        _cookButton.interactable = canCook;
    }

    public void Disable()
    {
        _recipe.Disable();
        _order = null;
        gameObject.SetActive(false);
    }

    public void SetStorages(KitchenBox box, TechnicManager technic)
    {
        box.IngredientsAdded += UpdateRecipe;
        _recipe.SetStorages(box, technic);
    }
}
