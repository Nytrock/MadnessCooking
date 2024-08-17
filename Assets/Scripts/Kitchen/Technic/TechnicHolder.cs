using UnityEngine;

[RequireComponent(typeof(TechnicHolderRenderer))]
public class TechnicHolder : MonoBehaviour {
    [SerializeField] private Technic _technic;
    [SerializeField] private TechnicHolderUI _UI;
    [SerializeField] private Transform _UITarget;
    [SerializeField] private TechnicHolderAnimator _animator;

    public TechnicHolderData Data { get; private set; }
    private KitchenUpgradeData _upgradeData;
    private TechnicHolderRenderer _renderer;

    public Transform UITarget => _UITarget;

    private void Awake() {
        _renderer = GetComponent<TechnicHolderRenderer>();
        _renderer.SetData(Data);
        _animator.SetData(Data);
    }

    private void Update() {
        Data.Update();
    }

    public virtual void ChangeState(bool newState) {
        _renderer.ChangeState(newState);
    }

    public void StartCook(Order order) {
        Data.StartCook(_upgradeData, order);
        _UI.StartWork();
        _renderer.UpdateVisual();
        _animator.UpdateAnimation();
    }

    private void StopCook() {
        if (!Data.IsCooking)
            return;

        _UI.StopWork();
        _renderer.UpdateVisual();
        _animator.UpdateAnimation();
    }

    public void StartRepair() {
        Data.StartRepair(_upgradeData);
        _UI.StartWork();
    }

    private void StopRepair() {
        _UI.StopWork();
        _renderer.UpdateVisual();
    }

    public void Bind(KitchenData data, int index) {
        _upgradeData = data.UpgradeData;

        data.TechnicHolders[index] ??= new(_technic);
        Data = data.TechnicHolders[index];
        _UI.SetData(Data);

        Data.CookStoped += StopCook;
        Data.RepairStoped += StopRepair;
    }

    public bool Accessible() {
        return !Data.IsCooking && Data.NowStrength != 0
            && !Data.IsRepairing;
    }

    public bool Repairable() {
        return Data.NowStrength != _technic.Strength &&
            MoneyManager.Instance.MoneyCount >= _technic.PriceRepair;
    }
}
