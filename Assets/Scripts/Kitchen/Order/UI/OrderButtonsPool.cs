using UnityEngine;

public class OrderButtonsPool : Pool<OrderButton> {
    [SerializeField] private OrderButton _prefab;
    [SerializeField] private TechnicManager _technicManager;
    [SerializeField] private KitchenStorage _kitchenStorage;
    [SerializeField] private HoverItemName _hoverText;

    protected override OrderButton CreateObject() {
        OrderButton button = Instantiate(_prefab, _container);
        button.SetManagers(_technicManager, _kitchenStorage);
        button.SetHoverText(_hoverText);
        return button;
    }

    public override void PutObject(OrderButton button) {
        base.PutObject(button);
        button.Disable();
    }
}
