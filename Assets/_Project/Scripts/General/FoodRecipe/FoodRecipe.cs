using UnityEngine;

public abstract class FoodRecipe<TPart> : MonoBehaviour
    where TPart : FoodRecipePart {
    [SerializeField] protected TPart[] _recipeParts = new TPart[8];
    [SerializeField] protected FoodRecipeTechnic _techicIcon;
    [SerializeField] protected KitchenStorage _kitchenStorage;
    [SerializeField] protected TechnicManager _technicManager;
    protected bool _canCook;
    protected Food _food;

    public bool CanCook => _canCook;

    public virtual void SetupRecipe(Food food) {
        DisableParts();
        _canCook = true;
        _food = food;

        SetupIngredients();
        SetupTechnic();
    }

    public virtual void SetHoverText(HoverItemName hoverText) {
        foreach (var recipePart in _recipeParts)
            recipePart.SetHoverText(hoverText);
        _techicIcon.SetHoverText(hoverText);
    }

    public virtual void DisableParts() {
        foreach (var part in _recipeParts)
            part.gameObject.SetActive(false);
        _techicIcon.ChangeState(false);
    }

    protected abstract void SetupIngredients();
    protected abstract void SetupTechnic();
}
