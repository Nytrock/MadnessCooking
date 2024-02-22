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
    
    private Order _order;
    private OrdersUI _ordersUI;
    private OrderCookingSlider _cookSlider;

    private void Start()
    {
        StartNewCycle();
    }

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
        _order = order;
        _order.OrderFinished += FinishCook;

        _icon.sprite = _order.Food.Icon;
        _title.text = _order.Food.Name;
        _tableCount.text = _order.TableNumber.ToString();

        var canCook = _recipe.CreateRecipe(_order);
        _cookButton.interactable = canCook;
    }

    public void UpdateRecipe()
    {
        if (_order == null) return;
        if (_order.IsCooking || _order.IsFinished) return;

        var canCook = _recipe.CreateRecipe(_order);
        _cookButton.interactable = canCook;
    }

    public void Disable()
    {
        _recipe.Disable();
        _order = null;
        gameObject.SetActive(false);
    }

    public void SetStorages(OrdersManager manager, OrdersUI ordersUI)
    {
        _cookSlider = GetComponent<OrderCookingSlider>();

        manager.KitchenStorage.IngredientsChanged += UpdateRecipe;
        _ordersUI = ordersUI;
        _cookSlider.SetTechnicManager(manager.TechnicManager);
        _recipe.SetStorages(manager.KitchenStorage, manager.TechnicManager);
    }

    public void Cook()
    {
        _cookingSlider.SetActive(true);
        _startButton.SetActive(false);
        _recipe.Disable();

        _order.StartCook();
        _cookSlider.StartCook(_order);
        _ordersUI.StartCook(_order);
    }

    public void FinishCook()
    {
        _cookingSlider.SetActive(false);
        _finishText.SetActive(true);
        _order = null;
    }
}
