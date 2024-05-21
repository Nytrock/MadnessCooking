using System;

[Serializable]
public class SerializableCarWaitManager
{
    public IngredientCountList IngredientsSended = new();
    public CarState CarState = CarState.Calm;
    public float NowTime;
    public float WaitTime;
}
