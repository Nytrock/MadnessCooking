using System;
using System.Collections.Generic;

[Serializable]
public class KitchenData : ISaveable
{
    public List<Food> AvailableFood;
    public SerializableIngredientStorage KitchenStorage;
    public List<Decor> AvailableDecor;

    public List<Technic> AvailableTechnic;
    public SerializableTechnic[] AllTechnic = new SerializableTechnic[TechnicManager.HoldersCount];

    public bool IsAutoSpice;
    public bool IsStrengthShow;
    public float TechnicCookSpeed = 1;
    public float TechnicRepairSpeed = 1;
    public float TechnicStrength = 1;
}
