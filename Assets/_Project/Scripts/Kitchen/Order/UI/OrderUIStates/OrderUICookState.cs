using UnityEngine;
using UnityEngine.UI;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public class OrderUICookState : OrderUIBaseState {
        [SerializeField] private Slider _cookSlider;

        private Order _order;
        private bool _isCook;

        public void SetOrder(Order order) {
            _order = order;
        }

        private void Update() {
            if (!_isCook)
                return;

            _cookSlider.value = _order.CookProgress;
        }

        public override void UpdateState(OrderUIState newState) {
            base.UpdateState(newState);
            _isCook = newState == _state;
        }
    }
}
