using System;

[Serializable]
public class SerializableFarmBed
{
    public BedType BedType;
    public Ingredient PlantedIngredient = null;
    public bool IsActive;

    public float NowTime = 0;
    public int Count = 0;
    public bool IsFull;
    public float AnimationTime;

    public SerializableBooster WaterBoost = new();
    public SerializableBooster FertilizeBoost = new();
    public SerializablePestsGenerator PestsGenerator = new();

    public float IndependentBoost = 1;
    public float PestsSlowdown = 1;
    
    public float SummarizedBoost => WaterBoost.Boost * FertilizeBoost.Boost 
        * IndependentBoost * PestsSlowdown;
}