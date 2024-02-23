public class OrderRecipe : FoodRecipe
{
    public void SetStorages(KitchenStorage ingredients, TechnicManager technic)
    {
        _kitchenStorage = ingredients;
        _technicManager = technic;
    }
}
