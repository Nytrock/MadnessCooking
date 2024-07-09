using UnityEngine;

[RequireComponent(typeof(TechnicHolderRenderer))]
public class TechnicHolder : MonoBehaviour {
    [SerializeField] private Technic _technic;
    [SerializeField] private TechnicHolderUI _UI;
    [SerializeField] private Transform _UITarget;

    public TechnicHolderData Data { get; private set; }
    private KitchenUpgradeData _upgradeData;
    private TechnicHolderRenderer _renderer;

    public Technic Technic => _technic;
    public Transform UITarget => _UITarget;

    private void Awake() {
        _renderer = GetComponent<TechnicHolderRenderer>();
    }

    private void Update() {
        Data.Update();
    }

    public void ChangeState(bool newState) {
        gameObject.SetActive(newState);
    }

    public void StartCook(Order order) {
        Data.StartCook(_upgradeData, order);
        _UI.StartWork();
        _renderer.UpdateVisual(Data);
    }

    private void StopCook() {
        if (!Data.IsCooking)
            return;

        _UI.StopWork();
        _renderer.UpdateVisual(Data);
    }

    public void StartRepair() {
        Data.StartRepair(_upgradeData);
        _UI.StartWork();
        _renderer.UpdateVisual(Data);
    }

    private void StopRepair() {
        _UI.StopWork();
        _renderer.UpdateVisual(Data);
    }

    public void Bind(KitchenData data, int index, bool isFileEmpty) {
        _upgradeData = data.UpgradeData;

        if (isFileEmpty)
            data.TechnicHolders[index] = new(_technic);
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

    public void SetRepairUI(TechnicRepairUI repairUI) {
        _UI.SetRepairUI(repairUI, this);
    }
}
