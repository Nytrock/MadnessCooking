using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Animator))]
public class KitchenCat : DecorHolder, IBindable<KitchenData> {
    [SerializeField, Min(0)] private float _needTime;
    [SerializeField, Min(0)] private float _fatigueDecreaseCoef;
    [SerializeField] private KitchenCatEyes _eyes;
    [SerializeField] private KitchenCatData _data;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void OnMouseDown() {
        if (_data.IsPetted || FatigueManager.Instance.IsTired)
            return;

        _animator.SetTrigger("isPet");
        _eyes.ChangeState(false);
        FatigueManager.Instance.ChangeFatigue(-_fatigueDecreaseCoef);
        _data.Pet();
    }

    private void Update() {
        if (_data.IsPetted)
            UpdateEyes();
        _data.Update();
    }

    public void Bind(KitchenData data) {
        data.Cat ??= new(_needTime);
        _data = data.Cat;
        UpdateEyes();
    }

    private void UpdateEyes() {
        _eyes.UpdateScale(_data);
    }

    public void EnableEyes() {
        _eyes.ChangeState(true);
    }
}
