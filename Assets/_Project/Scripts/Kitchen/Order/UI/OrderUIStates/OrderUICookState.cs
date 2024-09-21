using UnityEngine;

public class OrderUICookState : OrderUIBaseState {
    [SerializeField] private OrderCookingSlider _cookSlider;
    private Order _order;

    public void SetOrder(Order order) {
        _order = order;
    }

    public override void UpdateState(OrderUIState newState) {
        base.UpdateState(newState);
        if (newState == _state)
            _cookSlider.StartCook(_order);
    }
}
