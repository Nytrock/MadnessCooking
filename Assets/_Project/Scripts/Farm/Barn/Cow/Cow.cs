using UnityEngine;

public class Cow : NeedHoldAdd {
    [SerializeField] private IngredientManager _ingredientsManager;
    [SerializeField] private Puncher _puncher;
    [SerializeField, Min(0)] private float _wastePassiveAmount;
    [SerializeField, Min(0)] private float _wasteActiveAmount;

    protected override void Update() {
        base.Update();
        if (!Data.IsUnlocked)
            return;

        _puncher.AddWaste(_wastePassiveAmount * FpsManager.NORMALIZED_DELTA_TIME);
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
