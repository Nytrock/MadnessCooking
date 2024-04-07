using UnityEngine;

public class FarmBedUIManager : MonoBehaviour
{
    [SerializeField] private BedTypeUI[] _bedsUI;
    [SerializeField] private FarmBedUpgradeUI _upgrade;
    [SerializeField] private FarmWell _farmWell;
    [SerializeField] private Puncher _puncher;
    [SerializeField] private PestsRemoverUI _pestsRemoverUI;
    [SerializeField] private IngredientChoiceUI _ingredientChoice;

    private FarmBed _farmBed;
    private BedTypeUI _nowUI;


    private void Start()
    {
        _farmWell.WaterChanged += CheckWater;
        _puncher.FertilizeChanged += CheckFertilize;
    }

    public void ChangeMode()
    {
        _nowUI.ChangeMode();
    }

    private void CheckWater(int count)
    {
        if (_nowUI == null) return;

        _nowUI.CheckWater(count);
    }

    private void CheckFertilize(int count)
    {
        if (_nowUI == null) return;

        _nowUI.CheckFertilize(count);
    }

    public void UpdateCount()
    {
        _nowUI.UpdateCount(_farmBed.Count);
    }

    public void ShowGroundBed(FarmBed farmBed)
    {
        if (_farmBed == farmBed) {
            _nowUI.ChangeMode();
            return;
        }

        if (_nowUI != null) {
            _nowUI.ChangeMode(false);
            if (_farmBed != null)
                UpdateSideButtons();
        }

        _nowUI = FindUI(farmBed.BedType);
        _nowUI.UpdateInfo(farmBed);
        _nowUI.ChangeMode(true);

        if (_farmBed != null)
            _farmBed.CountChanged -= UpdateCount;

        transform.position = farmBed.transform.position;
        _farmBed = farmBed;
        _farmBed.CountChanged += UpdateCount;
        UpdateSideButtons();

        if (_nowUI.IsSideButtonsWork) {
            CheckWater(_farmWell.Count);
            CheckFertilize(_puncher.Count);
        }
    }

    private BedTypeUI FindUI(BedType bedType)
    {
        foreach (var bed in _bedsUI) {
            if (bed.BedType == bedType)
                return bed;
        }

        return null;
    }

    public void ActivateIngredientChoice(FarmBed groundBed)
    {
        _ingredientChoice.ActivateIngredientChoice(groundBed);
    }

    public void CollectIngredients()
    {
        _farmBed.SendIngredients();
    }

    public void ChangeBedType()
    {
        ChangeMode();
        _farmBed.GetComponent<BedChoice>().ReactivateBedsChoice();
    }

    public void ChangeIngredient()
    {
        ChangeMode();
        _farmBed.ResetIngredient();
        _ingredientChoice.ActivateIngredientChoice(_farmBed);
        _farmBed.CountChanged -= UpdateCount;
        _farmBed = null;
    }

    public void OpenUpgradesPanel()
    {
        _upgrade.ActivateUpgradePanel(_farmBed);
    }

    public void Water()
    {
        _farmWell.SubtractWater();
        _farmBed.Water();
    }

    public void Fertilize()
    {
        _puncher.SubtractFertilize();
        _farmBed.Fertilize();
    }

    public void Pests() 
    {
        var upgrader = _farmBed.Upgrader;
        if (upgrader.IsPestsInstant) {
            _farmBed.PestsGenerator.CleanPests();
        } else {
            _pestsRemoverUI.Activate(_farmBed.BedType, _farmBed.PestsGenerator);
        }
    }

    public void UpdateSideButtons()
    {
        _nowUI.UpdateSideButtons(_farmBed);
    }
}
