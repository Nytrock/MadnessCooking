using System.Collections.Generic;
using UnityEngine;

public class OrdersUI : MonoBehaviour, IUpgradeable
{
    [SerializeField] private OrdersManager _manager;
    [SerializeField] private OrdersPool _pool;
    [SerializeField] private GameObject _panel;
    private List<OrderButton> _orderButtons = new();

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _autoSpice;
    [SerializeField] private Ingredient _spice;
    public bool IsAutoSpice { get; private set; }

    private void Start()
    {
        _manager.OrderAdded += AddOrder;
        _manager.OrderRemoved += RemoveOrder;
        _pool.SetStorages(_manager, this);
        _panel.SetActive(false);
    }

    public void ChangeState()
    {
        _panel.SetActive(!_panel.activeSelf);
    }

    private void AddOrder(Order order)
    {
        order.OrderFinished += UpdateRecipes;
        OrderButton button = _pool.GetObject();
        button.StartSetup(order);
        _orderButtons.Add(button);
    }

    private void RemoveOrder(Order order)
    {
        int index = _manager.GetOrderId(order);
        _pool.PutObject(_orderButtons[index]);
        _orderButtons.RemoveAt(index);
    }

    public void StartCook(Order order)
    {
        _manager.StartCook(order);
        var ingredients = order.Food.Ingredients;
        for (int i = 0; i < ingredients.Size; i++) {
            if (ingredients.Get(i).Ingredient == _spice && IsAutoSpice) {
                MoneyManager.instance.ChangeMoney(-_spice.Cost * ingredients.Get(i).Count);
                break;
            }
        }

        UpdateRecipes();
    }

    private void UpdateRecipes()
    {
        foreach (OrderButton button in _orderButtons)
            button.UpdateRecipe();
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _autoSpice) {
            IsAutoSpice = true;
            UpdateRecipes();
        }
    }
}
