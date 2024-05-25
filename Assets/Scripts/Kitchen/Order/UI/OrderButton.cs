using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OrderCookingSlider))]
public class OrderButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _tableIndexText;

    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _cookingSlider;
    [SerializeField] private GameObject _finishText;

    [SerializeField] private OrderRecipe _recipe;
    [SerializeField] private Button _cookButton;
    
    private OrderCookingSlider _cookSlider;

    public Order Order { get; private set; }

    public void StartNewCycle()
    {
        _cookingSlider.SetActive(false);
        _startButton.SetActive(true);
        _finishText.SetActive(false);
        gameObject.SetActive(true);
    }

    public void SetOrder(Order order, KitchenData data)
    {
        StartNewCycle();
        Order = order;
        Order.OrderFinished += FinishCook;

        _icon.sprite = Order.Food.Icon;
        _titleText.text = Order.Food.Name;
        _tableIndexText.text = Order.TableIndex.ToString();

        _recipe.SetupRecipe(Order.Food, data);
        _cookButton.interactable = _recipe.CanCook;
    }

    public void UpdateRecipe()
    {
        if (Order == null) return;
        if (Order.IsCooking || Order.IsFinished) return;

        _recipe.SetupRecipe(Order.Food);
        _cookButton.interactable = _recipe.CanCook;
    }

    public void Disable()
    {
        _recipe.DisableParts();
        Order = null;
        gameObject.SetActive(false);
    }

    public void Setup(TechnicManager technicManager, KitchenStorage kitchenStorage)
    {
        _cookSlider = GetComponent<OrderCookingSlider>();

        kitchenStorage.IngredientsChanged += UpdateRecipe;
        technicManager.TechnicChanged += UpdateRecipe;

        _cookSlider.SetTechnicManager(technicManager);
        _recipe.Setup(kitchenStorage, technicManager);
    }

    public void Cook()
    {
        _cookingSlider.SetActive(true);
        _startButton.SetActive(false);
        _recipe.DisableParts();
        _cookSlider.StartCook(Order);
        Order.StartCook();
    }

    public void FinishCook()
    {
        _startButton.SetActive(false);
        _recipe.DisableParts();

        _cookingSlider.SetActive(false);
        _finishText.SetActive(true);
        Order = null;
    }
}
