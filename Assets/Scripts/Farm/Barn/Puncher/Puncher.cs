using System;
using UnityEngine;

public class Puncher : MonoBehaviour, IBindable<FarmData> {
    [SerializeField, Min(0)] private float _needWaste;

    [Header("Upgrades")]
    [SerializeField] private UpgradeManager _upgradeManager;
    [SerializeField] private CoefficientUpgrade _puncherSpeedUp;

    public PuncherData Data { get; private set; }

    public event Action FertilizerChanged;

    private void Awake() {
        _upgradeManager.ItemAdded += CheckSpeedChanged;
    }

    private void LateStart() {
        Data.SetNeedWaste(_needWaste);
        FertilizerChanged?.Invoke();
    }

    public void AddWaste(float wasteAmount) {
        Data.AddWaste(wasteAmount);
        FertilizerChanged?.Invoke();
    }

    public void SubtractFertilizer() {
        Data.SubtractFertilizer();
        FertilizerChanged?.Invoke();
    }

    private void CheckSpeedChanged(BaseUpgrade upgrade) {
        if (upgrade == _puncherSpeedUp)
            Data.ChangeSpeed(_puncherSpeedUp);
    }

    public void Bind(FarmData data) {
        data.Puncher ??= new();
        Data = data.Puncher;
        LateStart();
    }
}
