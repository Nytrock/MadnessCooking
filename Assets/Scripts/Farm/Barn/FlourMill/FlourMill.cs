using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FlourMill : NeedHoldAdd, IUpgradeable<FarmUpgradeData> {
    [SerializeField] private IngredientsManager _ingredientsManager;
    [SerializeField] private Puncher _puncher;
    [SerializeField, Min(0)] private float _wasteAmount;

    private NeedHoldAddData _cowData;
    private FarmUpgradeData _upgradeData;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    protected override void AddReady() {
        if (!_upgradeData.IsWheatDistributing)
            _cowData.SubstractMaterial();
        _puncher.AddWaste(_wasteAmount);
        base.AddReady();
    }

    public override void ChangeWorkMode(bool newValue) {
        _animator.SetBool("isHold", newValue && _needHoldData.MaterialCount > 0);
        base.ChangeWorkMode(newValue);
    }

    public override void Bind(FarmData data) {
        data.FlourMill ??= new();
        _data = data.FlourMill;

        _cowData = data.Cow;
        base.Bind(data);
    }

    public void BindUpgrade(FarmUpgradeData upgradeData) {
        _upgradeData = upgradeData;
    }

    protected override void UpdateUpgrades() {
        base.UpdateUpgrades();
        if (_data.IsUnlocked)
            _ingredientsManager.AddItem(ConstIngredients.Instance.Flour);
    }
}
