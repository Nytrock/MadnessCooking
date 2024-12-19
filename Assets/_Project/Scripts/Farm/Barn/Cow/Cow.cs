using UnityEngine;

public class Cow : NeedHoldAdd {
    [SerializeField] private IngredientsManager _ingredientsManager;
    [SerializeField] private Puncher _puncher;
    [SerializeField, Min(0)] private float _wastePassiveAmount;
    [SerializeField, Min(0)] private float _wasteActiveAmount;

    protected override void Update() {
        base.Update();
        _puncher.AddWaste(_wastePassiveAmount);
    }

    protected override void AddReady() {
        _puncher.AddWaste(_wasteActiveAmount);
        base.AddReady();
    }

    public override void Bind(FarmData data) {
        data.Cow ??= new(_readyDefaultCount, _materialDefaultCount);
        Data = data.Cow;
    }

    protected override void UpdateUpgrades() {
        base.UpdateUpgrades();
        if (Data.IsUnlocked)
            _ingredientsManager.AddItem(ConstIngredients.Instance.Milk);
    }
}
