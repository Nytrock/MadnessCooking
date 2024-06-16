using UnityEngine;

public class FarmBedUIManager : MonoBehaviour {
    [SerializeField] private BedTypeUI[] _bedsUI;
    [SerializeField] private FarmBedUpgraderUI _upgrade;
    [SerializeField] private FarmWell _farmWell;
    [SerializeField] private Puncher _puncher;
    [SerializeField] private PestsRemoverUI _pestsRemoverUI;
    [SerializeField] private IngredientChoiceUI _ingredientChoice;

    private FarmBed _farmBed;
    private BedTypeUI _nowUI;

    private void Awake() {
        _farmWell.WaterChanged += CheckWater;
        _puncher.FertilizeChanged += CheckFertilize;
    }

    public void ChangeMode() {
        _nowUI.ChangeMode();
    }

    private void CheckWater() {
        if (_nowUI == null) return;

        _nowUI.CheckWater();
    }

    private void CheckFertilize() {
        if (_nowUI == null) return;

        _nowUI.CheckFertilize();
    }

    public void ChangeState(FarmBed farmBed) {
        if (_farmBed == farmBed) {
            _nowUI.ChangeMode();
            return;
        }

        if (_nowUI != null) {
            _nowUI.ChangeMode(false);
            if (_farmBed != null) {
                _farmBed.CountChanged -= _nowUI.UpdateCount;
                UpdateSideButtons();
            }
        }

        _nowUI = FindUI(farmBed.BedData.BedType);
        _nowUI.UpdateInfo(farmBed);
        _nowUI.ChangeMode(true);

        transform.position = farmBed.transform.position;
        _farmBed = farmBed;
        _farmBed.CountChanged += _nowUI.UpdateCount;
        UpdateSideButtons();
    }

    private BedTypeUI FindUI(BedType bedType) {
        foreach (var bed in _bedsUI) {
            if (bed.BedType == bedType)
                return bed;
        }

        return null;
    }

    public void ActivateIngredientChoice(FarmBed groundBed) {
        _ingredientChoice.ActivateIngredientChoice(groundBed);
    }

    public void CollectIngredients() {
        _farmBed.SendIngredients();
    }

    public void ChangeBedType() {
        ChangeMode();
        _farmBed.ResetBedType();
    }

    public void ChangeIngredient() {
        ChangeMode();
        _farmBed.ResetIngredient();
        _ingredientChoice.ActivateIngredientChoice(_farmBed);
        _farmBed.CountChanged -= _nowUI.UpdateCount;
        _farmBed = null;
    }

    public void OpenUpgradesPanel() {
        _upgrade.ActivateUpgradePanel(_farmBed);
    }

    public void Water() {
        _farmWell.SubtractReady();
        _farmBed.Water();
    }

    public void Fertilize() {
        _puncher.SubtractReady();
        _farmBed.Fertilize();
    }

    public void Pests() {
        if (_farmBed.BedData.PestsGenerator.IsPestsInstant)
            _farmBed.PestsGenerator.CleanPests();
        else
            _pestsRemoverUI.Activate(_farmBed.BedData.BedType, _farmBed.PestsGenerator);
    }

    public void UpdateSideButtons() {
        _nowUI.UpdateSideButtons(_farmBed);
    }

    public void Bind(FarmData data) {
        foreach (var bedType in _bedsUI)
            bedType.Bind(data);
    }
}
