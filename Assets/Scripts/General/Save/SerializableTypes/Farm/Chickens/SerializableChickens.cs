using System;
using System.Collections.Generic;

[Serializable]
public class SerializableChickens
{
    public List<SerializableChickenFood> FoodList = new();
    public bool IsUnlocked;
    public float NowTime;
    public float Speed = 1;

    public int EggCount;
    public int FoodCount;
    public bool IsFeed;
    public bool IsInfiniteFood;
}
