using UnityEngine;

public class KitchenBox : IngredientStorage
{
    public void AddIngrediens(IngredientCountList countList)
    {
        for (int i = 0; i < countList.Size; i++)
            PutIngredient(countList.Get(i));
    }
}
