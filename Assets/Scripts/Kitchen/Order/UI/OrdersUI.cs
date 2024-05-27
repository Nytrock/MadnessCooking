using System.Collections.Generic;
using UnityEngine;

public class OrdersUI : MonoBehaviour, IUpgradeable, IBindable<KitchenData> 
{
    [SerializeField] private OrdersManager _ordersManager;
    [SerializeField] private OrderButtonsPool _pool;
    [SerializeField] private GameObject _panel;

    private readonly List<OrderButton> _orderButtons = new();
    private KitchenData _data;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _autoSpice;
    private Ingredient _spice;

    public bool IsAutoSpice => _data.IsAutoSpice;

    private void Awake()
    {
        _ordersManager.OrderAdded += AddOrder;
        _ordersManager.OrderRemoved += RemoveOrder;
        _panel.SetActive(false);
    }

    private void Start() {
        _spice = ConstIngredients.Instance.Spice;
    }

    public void ChangeState()
    {
        _panel.SetActive(!_panel.activeSelf);
    }

    private void AddOrder(Order order)
    {
        order.OrderStarted += delegate { StartCook(order); };
        order.OrderFinished += UpdateRecipes;
        OrderButton button = _pool.GetObject();
        button.SetOrder(order, _data);
        _orderButtons.Add(button);
    }

    private void RemoveOrder(Order order)
    {
        int index = _ordersManager.GetOrderIndex(order);
        _pool.PutObject(_orderButtons[index]);
        _orderButtons.RemoveAt(index);
    }

    public void StartCook(Order order)
    {
        _ordersManager.StartCook(order);
        IngredientCountList ingredients = order.Food.Ingredients;
        for (int i = 0; i < ingredients.Size; i++) {
            if (ingredients.Get(i).Ingredient == _spice && _data.IsAutoSpice) {
                MoneyManager.Instance.ChangeMoney(-_spice.Price * ingredients.Get(i).Count);
                break;
            }
        }

        UpdateRecipes();
    }

    private void UpdateRecipes()
    {
        foreach (var button in _orderButtons)
            button.UpdateRecipe();
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _autoSpice) {
            _data.IsAutoSpice = true;
            UpdateRecipes();
        }
    }

    public void Bind(KitchenData data, bool isFileEmpty)
    {
        _data = data;
        foreach (var button in _orderButtons) {
            if (button.Order.IsCooking)
                button.Cook();
            else if (button.Order.IsFinished)
                button.Order.FinishCook();
        }
    }
}
