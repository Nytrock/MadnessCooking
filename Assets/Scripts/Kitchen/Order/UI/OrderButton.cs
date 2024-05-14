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
    
    private OrdersUI _ordersUI;
    private OrderCookingSlider _cookSlider;

    public Order Order { get; private set; }

    public void StartNewCycle()
    {
        _cookingSlider.SetActive(false);
        _startButton.SetActive(true);
        _finishText.SetActive(false);
        gameObject.SetActive(true);
    }

    public void StartSetup(Order order)
    {
        StartNewCycle();
        Order = order;
        Order.OrderFinished += FinishCook;

        _icon.sprite = Order.Food.Icon;
        _title.text = Order.Food.Name;
        _tableCount.text = Order.TableNumber.ToString();

        _recipe.SetupRecipe(Order.Food, _ordersUI.IsAutoSpice);
        _cookButton.interactable = _recipe.CanCook;
    }

    public void UpdateRecipe()
    {
        if (Order == null) return;
        if (Order.IsCooking || Order.IsFinished) return;

        _recipe.SetupRecipe(Order.Food, _ordersUI.IsAutoSpice);
        _cookButton.interactable = _recipe.CanCook;
    }

    public void Disable()
    {
        _recipe.DisableParts();
        Order = null;
        gameObject.SetActive(false);
    }

    public void SetStorages(OrdersManager manager, OrdersUI ordersUI)
    {
        _cookSlider = GetComponent<OrderCookingSlider>();

        manager.KitchenStorage.IngredientsChanged += UpdateRecipe;
        manager.TechnicManager.TechnicChanged += UpdateRecipe;

        _ordersUI = ordersUI;
        _cookSlider.SetTechnicManager(manager.TechnicManager);
        _recipe.SetStorages(manager.KitchenStorage, manager.TechnicManager);
    }

    public void Cook()
    {
        _cookingSlider.SetActive(true);
        _startButton.SetActive(false);
        _recipe.DisableParts();

        Order.StartCook();
        _cookSlider.StartCook(Order);
        _ordersUI.StartCook(Order);
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
