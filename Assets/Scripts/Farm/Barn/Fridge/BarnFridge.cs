using UnityEngine;

public class BarnFridge : MonoBehaviour, IBindable<FarmData>
{
    [SerializeField] private FarmCar _car;
    [SerializeField] private Ingredient _milk;
    [SerializeField] private Ingredient _flour;

    public SerializableNeedHoldAdd Cow { get; private set; }
    public SerializableNeedHoldAdd FlourMill { get; private set; }

    public Ingredient Milk => _milk;
    public Ingredient Flour => _flour;

    public void PutIngredient(Ingredient ingredient)
    {
        if (ingredient != _milk && ingredient != _flour) {
            Debug.LogError("Unknown ingredient");
            return;
        }

        if (_car.Data.LeftSpace == 0)
            return;

        if (ingredient == _milk)
            FatigueManager.instance.ChangeFatigue(_milk.FatigueCount * Cow.ReadyCount);
        else if (ingredient == _flour)
            FatigueManager.instance.ChangeFatigue(_flour.FatigueCount * FlourMill.ReadyCount);
        MoveToCar(ingredient);
    }

    private void MoveToCar(Ingredient ingredient) {
        SerializableNeedHoldAdd changingHoldAdd;
        if (ingredient == _milk)
            changingHoldAdd = Cow;
        else
            changingHoldAdd = FlourMill;

        if (_car.Data.LeftSpace < changingHoldAdd.ReadyCount) {
            changingHoldAdd.ReadyCount -= _car.Data.LeftSpace;
            _car.PutIngredient(new IngredientCount(ingredient, _car.Data.LeftSpace));
        } else {
            _car.PutIngredient(new IngredientCount(ingredient, changingHoldAdd.ReadyCount));
            changingHoldAdd.ReadyCount = 0;
        }
    }

    public void Bind(FarmData data, bool isFileEmpty)
    {
        Cow = data.Cow;
        FlourMill = data.FlourMill;
    }
}
