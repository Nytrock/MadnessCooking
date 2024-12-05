using UnityEngine;

public class FarmBedUIManager : MonoBehaviour {
    [SerializeField] private LocationManager _locationManager;
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

    private void Awake() {
        _farmWell.WaterChanged += CheckWater;
        _puncher.FertilizerChanged += CheckFertilize;
        _locationManager.LocationChanged += delegate { ChangeMode(false); };
    }

    public void ChangeMode() {
        _nowUI.ChangeMode();
    }

    private void ChangeMode(bool newState) {
        if (_nowUI == null)
            return;

        _nowUI.ChangeMode(newState);
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


        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();

        if (_nowUI != null) {
            _nowUI.ChangeMode(false);
            if (_farmBed != null) {
                _farmBed.CountChanged -= _nowUI.UpdateCount;
                UpdateSideButtons();
            }
        }

        _nowUI = FindUI(farmBed.Data.BedType);
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

    public void ChangeBedTypeRequest() {
        _confirmPanel.StartConfirm(ChangeBedType, "FarmBedUI.RemoveBedConfirm");
    }

    private void ChangeBedType(bool confirmed) {
        if (!confirmed)
            return;

        ChangeMode();
        _farmBed.ResetBedType();
    }

    public void ChangeIngredientRequest() {
        _confirmPanel.StartConfirm(ChangeIngredient, "FarmBedUI.RemoveIngredientConfirm");
    }

    private void ChangeIngredient(bool confirmed) {
        if (!confirmed)
            return;

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
