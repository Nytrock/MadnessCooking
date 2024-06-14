using UnityEngine;

public abstract class FoodRecipe<T> : MonoBehaviour {
    [SerializeField] protected T[] _recipeParts = new T[8];
    [SerializeField] protected KitchenStorage _kitchenStorage;
    [SerializeField] protected TechnicManager _technicManager;
    protected bool _canCook;

    public bool CanCook => _canCook;

    public void SetupRecipe(Food food) {
        DisableParts();
        _canCook = true;

        SetupIngredients(food, ref _canCook);
        SetupTechnic(food.TypeTechnic, food.CountIngredients, ref _canCook);
    }

    public abstract void DisableParts();
    protected abstract void SetupIngredients(Food food, ref bool canCook);
    protected abstract void SetupTechnic(Technic technic, int index, ref bool canCook);
}
