using UnityEngine;

public class FoodRecipe : MonoBehaviour
{
    [SerializeField] private FoodRecipePart[] _recipeParts = new FoodRecipePart[8];
    [SerializeField] protected KitchenStorage _kitchenStorage;
    [SerializeField] protected TechnicManager _technicManager;
    protected bool _canCook;

    public bool CanCook => _canCook;

    public virtual void SetupRecipe(Food food)
    {
        DisableParts();
        var ingredients = food.Ingredients;
        _canCook = true;

        for (int i = 0; i < ingredients.Size; i++) {
            bool haveCount = _kitchenStorage.HaveCount(ingredients.Get(i));
            _canCook &= haveCount;
            _recipeParts[i].Setup(ingredients.Get(i), haveCount);
        }

        var technic = food.TypeTechnic;
        bool haveTechnic = _technicManager.HaveTechnic(technic);
        _canCook &= haveTechnic;
        _recipeParts[ingredients.Size].Setup(technic, haveTechnic);
    }

    public void DisableParts()
    {
        foreach (var part in _recipeParts)
            part.gameObject.SetActive(false);
    }
}
