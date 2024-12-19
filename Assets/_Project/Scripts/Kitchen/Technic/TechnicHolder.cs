using System;
using UnityEngine;

public class TechnicHolder : MonoBehaviour {
    [SerializeField] private Technic _technic;
    [SerializeField] private Transform _UITarget;
    [SerializeField] private TutorialManager _tutorialManager;

    private KitchenUpgradeData _upgradeData;

    [field: SerializeField] public TechnicHolderData Data { get; private set; }

    public Transform UITarget => _UITarget;
    public Technic Technic => _technic;

    public event Action<bool> StateChanged;
    public event Action RepairChanged;
    public event Action CookChanged;

    private void LateStart() {
        Data.CookStoped += StopCook;
        Data.RepairStoped += StopRepair;

        CookChanged?.Invoke();
        RepairChanged?.Invoke();
    }

    private void Update() {
        Data.Update();
    }

    public void ChangeState(bool newState) {
        StateChanged?.Invoke(newState);
    }

    public void StartCook(Order order) {
        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();

        Data.StartCook(_upgradeData, order);
        CookChanged?.Invoke();
    }

    private void StopCook() {
        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();
        CookChanged?.Invoke();
    }

    public void StartRepair() {
        Data.StartRepair(_upgradeData);
        RepairChanged?.Invoke();
    }

    private void StopRepair() {
        RepairChanged?.Invoke();
    }

    public void Bind(KitchenData data, int index) {
        _upgradeData = data.UpgradeData;

        data.TechnicHolders[index] ??= new(_technic);
        Data = data.TechnicHolders[index];

        LateStart();
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
