using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class FarmBedUIManager : MonoBehaviour, IStateable {
        [SerializeField] private UIActivatorsManager _activatorsManager;
        [SerializeField] private BedTypeUI[] _bedsUI;
        [SerializeField] private FarmBedUpgraderUI _upgrade;
        [SerializeField] private FarmWell _farmWell;
        [SerializeField] private Puncher _puncher;
        [SerializeField] private PestsRemoverUI _pestsRemoverUI;
        [SerializeField] private IngredientChoiceUI _ingredientChoice;
        [SerializeField] private ConfirmPanel _confirmPanel;
        [SerializeField] private TutorialManager _tutorialManager;

        private FarmBed _farmBed;
        private BedTypeUI _nowUI;

        public event Action<bool> StateChanged;

        private void Awake() {
            _farmWell.WaterChanged += CheckWater;
            _puncher.FertilizerChanged += CheckFertilize;
        }

        public void ChangeState(bool newState) {
            if (_nowUI == null)
                return;

            _nowUI.ChangeState(newState);
            StateChanged?.Invoke(newState);

            if (!newState)
                ResetNowUI();
        }

        private void CheckWater() {
            if (_nowUI == null) return;

            _nowUI.CheckWater();
        }

        private void CheckFertilize() {
            if (_nowUI == null) return;

            _nowUI.CheckFertilize();
        }

        public void SetFarmBed(FarmBed farmBed) {
            if (_farmBed == farmBed) {
                _activatorsManager.CloseNowActivable();
                return;
            }

            if (_tutorialManager.IsWork)
                _tutorialManager.NextTutorialPart();

            if (_nowUI != null)
                _activatorsManager.CloseNowActivable();

            _nowUI = FindUI(farmBed.Data.BedType);
            _nowUI.UpdateInfo(farmBed);
            _activatorsManager.SetActivable(this);

            transform.position = farmBed.transform.position;
            _farmBed = farmBed;
            _farmBed.CountChanged += _nowUI.UpdateCount;
            UpdateSideButtons();
        }

        private void ResetNowUI() {
            if (_farmBed == null) {
                _nowUI = null;
                return;
            }

            _farmBed.CountChanged -= _nowUI.UpdateCount;
            _nowUI = null;
            _farmBed = null;
        }

        private BedTypeUI FindUI(BedType bedType) {
            foreach (var bed in _bedsUI) {
                if (bed.BedType == bedType)
                    return bed;
            }

            return null;
        }

        public void ActivateIngredientChoice(FarmBed farmBed) {
            _ingredientChoice.ActivateIngredientChoice(farmBed);
        }

        public void CollectIngredients() {
            _farmBed.CollectIngredients();
        }

        public void RemoveBedRequest() {
            _confirmPanel.StartConfirm(RemoveBed, "FarmBedUI.RemoveBedConfirm");
        }

        private void RemoveBed(bool confirmed) {
            if (!confirmed)
                return;

            _farmBed.ResetBedType();
            _activatorsManager.CloseNowActivable();
        }

        public void ChangeIngredientRequest() {
            _confirmPanel.StartConfirm(ChangeIngredient, "FarmBedUI.RemoveIngredientConfirm");
        }

        private void ChangeIngredient(bool confirmed) {
            if (!confirmed)
                return;

            _farmBed.ResetIngredient();
            _ingredientChoice.ActivateIngredientChoice(_farmBed);
            _activatorsManager.CloseNowActivable();
        }

        public void OpenUpgradesPanel() {
            _upgrade.ActivateUpgradePanel(_farmBed);
        }

        public void Water() {
            _farmWell.SubtractReady();
            _farmBed.Water();
        }

        public void Fertilize() {
            _puncher.SubtractFertilizer();
            _farmBed.Fertilize();
        }

        public void Pests() {
            if (_farmBed.Data.PestsGenerator.IsPestsInstant)
                _farmBed.PestsGenerator.CleanPests();
            else
                _pestsRemoverUI.Activate(_farmBed.Data.BedType, _farmBed.PestsGenerator);
        }

        public void UpdateSideButtons() {
            _nowUI.UpdateSideButtons(_farmBed);
        }

        public void Bind(FarmData data) {
            foreach (var bedType in _bedsUI)
                bedType.Bind(data);
        }
    }
}
