using UnityEngine;

public abstract class FoodRecipe<T> : MonoBehaviour {
    [SerializeField] protected T[] _recipeParts = new T[8];
    [SerializeField] protected GrayscaleImageRenderer _techicIcon;
    [SerializeField] protected KitchenStorage _kitchenStorage;
    [SerializeField] protected TechnicManager _technicManager;
    protected bool _canCook;
    protected Food _food;

    public bool CanCook => _canCook;

    public void SetupRecipe(Food food) {
        DisableParts();
        _canCook = true;
        _food = food;

        SetupIngredients(ref _canCook);
        SetupTechnic(ref _canCook);
    }

    public abstract void DisableParts();
    protected abstract void SetupIngredients(ref bool canCook);
    protected abstract void SetupTechnic(ref bool canCook);
}
