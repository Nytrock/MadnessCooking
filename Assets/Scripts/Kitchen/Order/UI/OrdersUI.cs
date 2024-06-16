using System.Collections.Generic;
using UnityEngine;

public class OrdersUI : MonoBehaviour, IUpgradeable<KitchenUpgradeData>, IBindable<KitchenData> {
    [SerializeField] private OrdersManager _ordersManager;
    [SerializeField] private OrderButtonsPool _pool;
    [SerializeField] private GameObject _panel;

    private readonly List<OrderButton> _orderButtons = new();
    private KitchenUpgradeData _upgradeData;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _autoSpice;
    private Ingredient _spice;

    private void Awake() {
        _ordersManager.OrderAdded += AddOrder;
        _ordersManager.OrderRemoved += RemoveOrder;
        _panel.SetActive(false);
    }

    private void Start() {
        _spice = ConstIngredients.Instance.Spice;
    }

    public void ChangeState() {
        _panel.SetActive(!_panel.activeSelf);
    }

    private void AddOrder(Order order) {
        order.OrderStarted += delegate { StartCook(order); };
        order.OrderFinished += UpdateRecipes;
        OrderButton button = _pool.GetObject();
        button.SetOrder(order, _upgradeData);
        _orderButtons.Add(button);
    }

    private void RemoveOrder(Order order) {
        int index = _ordersManager.GetOrderIndex(order);
        _pool.PutObject(_orderButtons[index]);
        _orderButtons.RemoveAt(index);
    }

    public void StartCook(Order order) {
        _ordersManager.StartCook(order);
        foreach (var count in order.Food.Ingredients) {
            if (count.Ingredient == _spice && _upgradeData.IsAutoSpice) {
                MoneyManager.Instance.ChangeMoney(-_spice.Price * count.Count);
                break;
            }
        }

        UpdateRecipes();
    }

    private void UpdateRecipes() {
        foreach (var button in _orderButtons)
            button.UpdateRecipe();
    }

    public void Bind(KitchenData data, bool isFileEmpty) {
        foreach (var button in _orderButtons) {
            if (button.Order.IsCooking)
                button.Cook();
            else if (button.Order.IsFinished)
                button.Order.FinishCook();
        }
    }

    public void BindUpgrade(KitchenUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    public void CheckAddedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == _autoSpice) {
            _upgradeData.SetAutoSpice();
            UpdateRecipes();
        }
    }
}
