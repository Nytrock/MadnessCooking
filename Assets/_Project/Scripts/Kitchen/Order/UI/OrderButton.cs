using TMPro;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public class OrderButton : MonoBehaviour {
        [SerializeField] private HoverItemNameActivator _foodInfo;
        [SerializeField] private TextMeshProUGUI _tableIndexText;
        [SerializeField] private OrderRecipe _recipe;

        [Header("States")]
        [SerializeField] private OrderUIStartState _startState;
        [SerializeField] private OrderUICookState _cookState;
        [SerializeField] private OrderUIBaseState _finishState;

        private TechnicManager _technicManager;

        public Order Order { get; private set; }

        public void StartNewCycle() {
            ChangeState(OrderUIState.Start);
            gameObject.SetActive(true);
        }

        public void SetOrder(Order order, KitchenUpgradeData data) {
            StartNewCycle();
            Order = order;
            Order.OrderFinished += FinishCook;
            _cookState.SetOrder(order);

            _foodInfo.SetItem(Order.Food);
            _tableIndexText.text = Order.TableIndex.ToString();

            _recipe.SetupRecipe(Order.Food, data);
            UpdateCookSlider();

            if (order.IsCooking) {
                Cook();
                _technicManager.StartCooking(order);
            } else if (order.IsFinished) {
                FinishCook();
            }
        }

        public void SetManagers(TechnicManager technicManager, KitchenStorage kitchenStorage) {
            kitchenStorage.IngredientCountAdded += UpdateRecipeIngredients;
            kitchenStorage.IngredientCountRemoved += UpdateRecipeIngredients;
            technicManager.TechnicChanged += UpdateRecipeTechnic;
            MoneyManager.Instance.MoneyChanged += UpdateAutoSpices;

            _technicManager = technicManager;
            _recipe.SetManagers(kitchenStorage, technicManager);
        }

        public void SetHoverText(HoverTextPanel hoverText) {
            _foodInfo.SetHoverPanel(hoverText);
            _recipe.SetHoverText(hoverText);
        }

        private void UpdateRecipeIngredients(IngredientCount count) {
            if (CheckOrderStarted())
                return;

            _recipe.UpdateRecipeIngredients(count);
            UpdateCookSlider();
        }

        private void UpdateAutoSpices(int count) {
            if (CheckOrderStarted())
                return;

            _recipe.UpdateAutoSpices(count);
            UpdateCookSlider();
        }

        public void UpdateRecipeTechnic() {
            if (CheckOrderStarted())
                return;

            _recipe.UpdateRecipeTechnic();
            UpdateCookSlider();
        }

        public void Disable() {
            _recipe.DisableParts();
            Order = null;
            gameObject.SetActive(false);
        }

        public void Cook() {
            Order.StartCook();
            ChangeState(OrderUIState.Cook);
            _recipe.DisableParts();
        }

        public void FinishCook() {
            ChangeState(OrderUIState.Finish);
            _recipe.DisableParts();
        }

        private void UpdateCookSlider() {
            _startState.UpdateCookButton(_recipe.CanCook);
        }

        private void ChangeState(OrderUIState newState) {
            _startState.UpdateState(newState);
            _cookState.UpdateState(newState);
            _finishState.UpdateState(newState);
        }

        private bool CheckOrderStarted() {
            if (Order == null)
                return true;

            if (Order.IsCooking || Order.IsFinished)
                return true;

            return false;
        }
    }
}
