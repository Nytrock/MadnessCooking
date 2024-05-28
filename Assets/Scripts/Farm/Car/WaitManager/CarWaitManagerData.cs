using System;

[Serializable]
public class CarWaitManagerData {
    public IngredientCountList IngredientsSended = new();
    public CarState CarState = CarState.Calm;
    public float NowWaitTime;
    public float NeedWaitTime;
}
