using MadnessCooking.General;
using System;
using UnityEngine;

namespace MadnessCooking.Farm {
    public class Puncher : MonoBehaviour, ISaveable {
        [SerializeField, Min(0)] private float _needWaste;

        [Header("Upgrades")]
        [SerializeField] private UpgradeManager _upgradeManager;
        [SerializeField] private CoefficientUpgrade _puncherSpeedUp;

        [field: SerializeField] public PuncherData Data { get; private set; }

        public event Action FertilizerChanged;

        private void Awake() {
            _upgradeManager.ItemAdded += CheckSpeedChanged;
        }

        public void LateStart() {
            Data.SetNeedWaste(_needWaste);
            FertilizerChanged?.Invoke();
        }

        public void AddWaste(float wasteAmount) {
            int oldCount = Data.FertilizerCount;
            Data.AddWaste(wasteAmount);

            if (oldCount != Data.FertilizerCount)
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

        public void LoadSave(GameData data) {
            data.Farm.Puncher ??= new();
            Data = data.Farm.Puncher;
        }
    }
}
