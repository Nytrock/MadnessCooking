using UnityEngine;

public class OrderRecipe : MonoBehaviour
{
    [SerializeField] private OrderRecipePart[] _recipeParts = new OrderRecipePart[8];
    private KitchenStorage _kitchenStorage;
    private TechnicManager _technicManager;

    public bool CreateRecipe(Order order)
    {
        Disable();
        var ingredients = order.Food.Ingredients;
        bool canCook = true;

        for (int i = 0; i < ingredients.Length; i++) {
            bool haveCount = _kitchenStorage.HaveCount(ingredients[i]);
            canCook &= haveCount;
            _recipeParts[i].Setup(ingredients[i], haveCount);
        }

        var technic = order.Food.TypeTechnic;
        bool haveTechnic = _technicManager.HaveTechnic(technic);
        canCook &= haveTechnic;
        _recipeParts[ingredients.Length].Setup(technic, haveTechnic);
        return canCook;
    }

    public void Disable()
    {
        foreach (var part in _recipeParts)
            part.gameObject.SetActive(false);
    }

    public void SetStorages(KitchenStorage ingredients, TechnicManager technic)
    {
        _kitchenStorage = ingredients;
        _technicManager = technic;
    }
}