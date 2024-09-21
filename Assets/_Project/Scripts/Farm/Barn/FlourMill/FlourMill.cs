using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FlourMill : NeedHoldAdd {
    [SerializeField] private IngredientsManager _ingredientsManager;
    [SerializeField] private ParticleSystem _flourParticle;
    [SerializeField] private Puncher _puncher;
    [SerializeField, Min(0)] private float _wasteAmount;

    private Animator _animator;

    protected override void Awake() {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    protected override void AddReady() {
        _puncher.AddWaste(_wasteAmount);
        base.AddReady();
    }

    public override void ChangeWorkMode(bool newValue) {
        bool isWork = (newValue || Data.IsAuto) && NeedHoldData.MaterialCount > 0;
        ChangeAnimationState(isWork);
        base.ChangeWorkMode(newValue);
    }

    public override void Bind(FarmData data) {
        data.FlourMill ??= new(_readyDefaultCount, _materialDefaultCount);
        Data = data.FlourMill;
        base.Bind(data);
    }

    protected override void UpdateUpgrades() {
        base.UpdateUpgrades();
        if (Data.IsUnlocked)
            _ingredientsManager.AddItem(ConstIngredients.Instance.Flour);
        if (Data.IsAuto)
            ChangeAnimationState(true);
    }

    private void ChangeAnimationState(bool newState) {
        _animator.SetBool("isHold", newState);
        _flourParticle.ChangeState(newState);
    }
}
