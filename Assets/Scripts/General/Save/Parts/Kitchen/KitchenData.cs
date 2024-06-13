using System;
using System.Collections.Generic;

[Serializable]
public class KitchenData : ISaveable {
    public List<Food> AvailableFood;
    public IngredientStorageData KitchenStorage;
    public DecorManagerData DecorData = new();

    public List<Technic> AvailableTechnic;
    public TechnicData[] AllTechnic;

    public bool IsAutoSpice;
    public bool IsStrengthShow;
    public float TechnicCookSpeed = 1;
    public float TechnicRepairSpeed = 1;
    public float TechnicStrength = 1;
}
