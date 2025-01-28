using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrdersUI : MonoBehaviour, IUpgradeable<KitchenUpgradeData>, IActivable {
    [SerializeField] private OrdersManager _ordersManager;
    [SerializeField] private OrderButtonsPool _pool;
    [SerializeField] private GameObject _panel;

    [Header("Upgrades")]
    [SerializeField] private BaseUpgrade _autoSpice;

    private List<OrderButton> _orderButtons;
    private KitchenUpgradeData _upgradeData;
    private Ingredient _spice;

    public event Action<bool> StateChanged;

    private void Awake() {
        _ordersManager.OrderAdded += AddOrder;
        _ordersManager.OrderRemoved += RemoveOrder;
        _panel.SetActive(false);
        _orderButtons = new();
    }

    private void Start() {
        _spice = ConstIngredients.Instance.Spice;
    }

    public void ChangeState(bool newState) {
        _panel.SetActive(newState);
        StateChanged?.Invoke(newState);
    }

    private void AddOrder(Order order) {
        order.OrderStarted += delegate { StartCook(order); };
        order.OrderFinished += UpdateRecipes;
        OrderButton button = _pool.GetObject();
        button.SetOrder(order, _upgradeData);
        _orderButtons.Add(button);
        SortOrderButtons();
    }

    private void SortOrderButtons() {
        _orderButtons = _orderButtons.OrderBy(button => button.Order.TableIndex).ToList();
        for (int i = 0; i < _orderButtons.Count; i++)
            _orderButtons[i].transform.SetSiblingIndex(i);
    }

    private void RemoveOrder(Order order) {
        int index = _ordersManager.GetOrderIndex(order);
        _pool.PutObject(_orderButtons[index]);
        _orderButtons.RemoveAt(index);
    }

    public void StartCook(Order order) {
        _ordersManager.StartCook(order);
        foreach (var count in order.Food.Ingredients) {
            if (count.Item && _upgradeData.IsAutoSpice) {
                MoneyManager.Instance.ChangeMoney(-_spice.Price * count.Count);
                break;
            }
        }

        UpdateRecipes();
    }

    private void UpdateRecipes() {
        foreach (var button in _orderButtons)
            button.UpdateRecipeTechnic();
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
