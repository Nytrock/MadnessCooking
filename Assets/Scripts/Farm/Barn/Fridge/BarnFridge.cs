using System;
using UnityEngine;

public class BarnFridge : MonoBehaviour, IBindable<FarmData>
{
    [SerializeField] private FarmCar _car;
    private Ingredient _milk;
    private Ingredient _flour;

    public SerializableNeedHoldAdd Cow { get; private set; }
    public SerializableNeedHoldAdd FlourMill { get; private set; }

    private void Start() {
        _milk = IngredientsManager.Instance.Milk;
        _flour = IngredientsManager.Instance.Flour;
    }

    public void PutIngredient(Ingredient ingredient)
    {
        if (ingredient != _milk && ingredient != _flour)
            throw new ArgumentException("Unknown ingredient");

        if (_car.Data.LeftSpace == 0)
            return;

        if (ingredient == _milk)
            FatigueManager.Instance.ChangeFatigue(_milk.FatigueCount * Cow.ReadyCount);
        else if (ingredient == _flour)
            FatigueManager.Instance.ChangeFatigue(_flour.FatigueCount * FlourMill.ReadyCount);
        MoveToCar(ingredient);
    }

    private void MoveToCar(Ingredient ingredient) {
        SerializableNeedHoldAdd changingHoldAdd;
        if (ingredient == _milk)
            changingHoldAdd = Cow;
        else
            changingHoldAdd = FlourMill;

        int remainCount = _car.PutIngredientWithRemain(new IngredientCount(ingredient, changingHoldAdd.ReadyCount));
        FatigueManager.Instance.ChangeFatigue(ingredient.FatigueCount * (changingHoldAdd.ReadyCount - remainCount));
        changingHoldAdd.ReadyCount = remainCount;
    }

    public void Bind(FarmData data, bool isFileEmpty)
    {
        Cow = data.Cow;
        FlourMill = data.FlourMill;
    }
}
