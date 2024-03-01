using UnityEngine;

public class FarmBedUIManager : MonoBehaviour
{
    [SerializeField] private BedTypeUI[] _bedsUI;
    [SerializeField] private FarmBedUpgradeUI _upgrade;
    [SerializeField] private FarmWell _farmWell;
    [SerializeField] private Puncher _puncher;
    [SerializeField] private PestsRemoverUI _pestsRemoverUI;
    [SerializeField] private IngredientChoiceUI _ingredientChoice;

    private FarmBed _groundBed;
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
        _nowUI.UpdateCount(_groundBed.Count);
    }

    public void ShowGroundBed(FarmBed groundBed)
    {
        if (_groundBed == groundBed) {
            _nowUI.ChangeMode();
            return;
        }

        if (_nowUI != null)
            _nowUI.ChangeMode(false);

        _nowUI = FindUI(groundBed.BedType);
        _nowUI.UpdateInfo(groundBed);
        _nowUI.ChangeMode(true);

        if (_groundBed != null)
            _groundBed.CountChanged -= UpdateCount;

        transform.position = groundBed.transform.position;
        _groundBed = groundBed;
        _groundBed.CountChanged += UpdateCount;

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
        _ingredientChoice.Activate(groundBed);
    }

    public void CollectIngredients()
    {
        _groundBed.SendIngredients();
    }

    public void ChangeBedType()
    {
        ChangeMode();
        _groundBed.GetComponent<BedChoice>().ReactivateBedsChoice();
    }

    public void ChangeIngredient()
    {
        ChangeMode();
        _groundBed.ResetIngredient();
        _ingredientChoice.Activate(_groundBed);
    }

    public void OpenUpgradesPanel()
    {
        _upgrade.Activate(_groundBed);
    }

    public void Water()
    {
        _farmWell.SubtractWater();
        _groundBed.Water();
    }

    public void Fertilize()
    {
        _puncher.SubtractFertilize();
        _groundBed.Fertilize();
    }

    public void Pests() 
    {
        _pestsRemoverUI.Activate(_groundBed.BedType, _groundBed.GetPests());
    }

    public void ForgiveBed(FarmBed groundBed)
    {
        if (_groundBed == groundBed)
            _groundBed = null;
    }
}
