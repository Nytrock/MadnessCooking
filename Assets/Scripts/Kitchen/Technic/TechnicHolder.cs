using UnityEngine;

[RequireComponent(typeof(Animator), typeof(TechnicCooker), typeof(TechnicRepairer))]
public class TechnicHolder : MonoBehaviour {
    [SerializeField] private Technic _technic;
    [SerializeField] private Transform _UITarget;
    private Animator _animator;
    private Order _nowOrder;

    private TechnicCooker _cooker;
    private TechnicRepairer _repair;

    public TechnicHolderData TechnicData { get; private set; }
    private KitchenUpgradeData _upgradeData;

    public Technic Technic => _technic;
    public Transform UITarget => _UITarget;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _cooker = GetComponent<TechnicCooker>();
        _repair = GetComponent<TechnicRepairer>();
    }

    public void ChangeState(bool newState) {
        gameObject.SetActive(newState);
    }

    public void StartCook(Order order) {
        TechnicData.IsCooking = true;
        TechnicData.NowStrength = Mathf.Max(TechnicData.NowStrength - (Random.Range(1f, 2f) / _upgradeData.TechnicStrength), 0);
        _animator.SetBool("isCooking", true);

        _nowOrder = order;
        _cooker.StartWork(order.Food.TimeToCook / _upgradeData.TechnicCookSpeed);
    }

    public void StopCook() {
        if (!TechnicData.IsCooking)
            return;

        TechnicData.IsCooking = false;
        _animator.SetBool("isCooking", false);
        _nowOrder.FinishCook();
        _nowOrder = null;
    }

    public void StartRepair() {
        TechnicData.IsRepairing = true;
        _repair.StartWork(_technic.TimeRepair / _upgradeData.TechnicRepairSpeed);
    }

    public void StopRepair() {
        TechnicData.IsRepairing = false;
    }

    public void Bind(KitchenData data, int index, bool isFileEmpty) {
        _upgradeData = data.UpgradeData;

        if (isFileEmpty) {
            data.TechnicHolders[index] = new() {
                NowStrength = _technic.Strength
            };
        }

        TechnicData = data.TechnicHolders[index];
        if (TechnicData.IsRepairing)
            StartRepair();
    }

    public bool Accessible() {
        return !TechnicData.IsCooking && TechnicData.NowStrength != 0 && !TechnicData.IsRepairing;
    }

    public bool Repairable() {
        return TechnicData.NowStrength != _technic.Strength &&
            MoneyManager.Instance.MoneyCount >= _technic.PriceRepair;
    }
}
