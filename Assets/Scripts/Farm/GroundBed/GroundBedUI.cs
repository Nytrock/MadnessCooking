using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GroundBedUI : MonoBehaviour
{
    [SerializeField] private GroundBed _groundBed;
    [SerializeField] private GameObject _UI;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _countText;

    [Header("Side buttons")]
    [SerializeField] private GameObject _sideButtons;
    [SerializeField] private Button _waterButton;
    [SerializeField] private Button _fertilizeButton;
    [SerializeField] private Button _pestsButton;

    private BedChoice _bedChoice;
    private GroundBedUpgradeUI _upgrade;
    private FarmWell _farmWell;
    private Puncher _puncher;
    private PestsRemoverUI _pestsRemoverUI;


    private void Start()
    {
        _bedChoice = _groundBed.GetComponent<BedChoice>();
        _UI.SetActive(false);
    }

    public void ChangeMode()
    {
        _UI.SetActive(!_UI.activeSelf);
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
        _waterButton.interactable = count > 0;
    }

    private void CheckFertilize(int count)
    {
        _fertilizeButton.interactable = count > 0;
    }

    public void UpdateIngredient(Ingredient ingredient)
    {
        _icon.sprite = ingredient.IngredientSprite;
        _name.text = ingredient.Name;
        _description.text = ingredient.Description;
        _countText.text = "0";

        var sideButtonShow = !(_groundBed.BedType.AcceptableType == IngredientType.Meat ||
            _groundBed.BedType.AcceptableType == IngredientType.Ghost);
        _sideButtons.SetActive(sideButtonShow);

        if (sideButtonShow) {
            _farmWell.WaterAdded += CheckWater;
            _puncher.FertilizeAdded += CheckFertilize;
            CheckWater(_farmWell.Count);
            CheckFertilize(_puncher.Count);
        }
    }

    public void UpdateCount()
    {
        _countText.text = _groundBed.Count.ToString();
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
