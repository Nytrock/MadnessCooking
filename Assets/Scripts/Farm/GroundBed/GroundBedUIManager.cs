using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GroundBedUIManager : MonoBehaviour
{
    [SerializeField] private GroundBed _groundBed;
    private BedTypeHolderUI _nowUI;

    private BedChoice _bedChoice;
    private GroundBedUpgradeUI _upgrade;
    private FarmWell _farmWell;
    private Puncher _puncher;
    private PestsRemoverUI _pestsRemoverUI;


    private void Start()
    {
        _bedChoice = _groundBed.GetComponent<BedChoice>();
    }

    public void ChangeMode()
    {
        _nowUI.ChangeMode();
    }

    public void Setup(GroundBedSettings settings)
    {
        _upgrade = settings.UpgradeUI;
        _farmWell = settings.FarmWell;
        _puncher = settings.Puncher;
        _pestsRemoverUI = settings.PestsRemoverUI;
    }

    private void CheckWater(int count)
    {
        _nowUI.CheckWater(count);
    }

    private void CheckFertilize(int count)
    {
        _nowUI.CheckFertilize(count);
    }

    public void UpdateCount()
    {
        _nowUI.UpdateCount(_groundBed.Count);
    }

    public void UpdateIngredient(Ingredient ingredient, BedTypeHolder _bedHolder)
    {
        _nowUI = _bedHolder.HolderUI;
        _nowUI.UpdateIngredient(ingredient);

        if (_nowUI.IsSideButtonsWork) {
            _farmWell.WaterChanged += CheckWater;
            _puncher.FertilizeChanged += CheckFertilize;
            CheckWater(_farmWell.Count);
            CheckFertilize(_puncher.Count);
        } else {
            _farmWell.WaterChanged -= CheckWater;
            _puncher.FertilizeChanged -= CheckFertilize;
        }
    }

    public void CollectIngredients()
    {
        _groundBed.SendIngredients();
    }

    public void ChangeBedType()
    {
        ChangeMode();
        _bedChoice.ReactivateBedsChoice();
    }

    public void ChangeIngredient()
    {
        ChangeMode();
        _groundBed.ResetIngredient();
        _groundBed.ActivateIngredientChoice();
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
        _pestsRemoverUI.Activate(_groundBed);
    }
}
