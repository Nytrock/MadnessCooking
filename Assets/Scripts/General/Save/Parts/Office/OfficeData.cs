using System;
using System.Collections.Generic;

[Serializable]
public class OfficeData : ISaveable {
    public List<Decor> AvailableDecor = new();
    public bool IsSleeping;
    public float InternetDownloadSpeed = 1;
    public bool IsInternetDownloadInstant;

    public Ingredient[] ShopIngredients;
    public Technic[] ShopTechnic;
    public Food[] ShopFood;
    public Decor[] ShopDecor;
    public BaseUpgrade[] ShopUpgrades;
}
