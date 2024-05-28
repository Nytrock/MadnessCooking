using System;
using System.Collections.Generic;

[Serializable]
public class ChickensData {
    public List<ChickenFoodData> FoodList = new();
    public bool IsUnlocked;
    public float NowTime;
    public float Speed = 1;

    public int EggCount;
    public int FoodCount;
    public bool IsFeed;
    public bool IsInfiniteFood;
}
