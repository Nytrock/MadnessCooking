using TMPro;
using UnityEngine;

public class OrderButton : MonoBehaviour {
    [SerializeField] private BuyableItemRendererWithName _foodInfo;
    [SerializeField] private TextMeshProUGUI _tableIndexText;
    [SerializeField] private OrderRecipe _recipe;

    [Header("States")]
    [SerializeField] private OrderUIStartState _startState;
    [SerializeField] private OrderUICookState _cookState;
    [SerializeField] private OrderUIBaseState _finishState;

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

        _foodInfo.SetItemInfo(Order.Food);
        _tableIndexText.text = Order.TableIndex.ToString();

        _recipe.SetupRecipe(Order.Food, data);
        UpdateCookSlider();

        if (order.IsCooking)
            Cook();
        else if (order.IsFinished)
            FinishCook();
    }

    public void SetManagers(TechnicManager technicManager, KitchenStorage kitchenStorage) {
        kitchenStorage.IngredientCountAdded += UpdateRecipeIngredients;
        technicManager.TechnicChanged += UpdateRecipeTechnic;
        MoneyManager.Instance.MoneyChanged += UpdateAutoSpices;

        _recipe.SetManagers(kitchenStorage, technicManager);
        _cookState.SetTechnicManager(technicManager);
    }

    public void SetHoverText(HoverItemName hoverText) {
        _recipe.SetHoverText(hoverText);
    }

    private void UpdateRecipeIngredients(BuyableItemCount<Ingredient> count) {
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
        ChangeState(OrderUIState.Cook);
        _recipe.DisableParts();
        Order.StartCook();
    }

    public void FinishCook() {
        ChangeState(OrderUIState.Finish);
        _recipe.DisableParts();
        Order = null;
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
