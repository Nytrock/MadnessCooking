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
    private BedChoice _bedChoice;
    private int _count;


    private void Start()
    {
        _bedChoice = _groundBed.GetComponent<BedChoice>();
        _UI.SetActive(false);
    }

    public void ChangeMode()
    {
        _UI.SetActive(!_UI.activeSelf);
    }

    public void UpdateIngredient(Ingredient ingredient)
    {
        _icon.sprite = ingredient.IngredientSprite;
        _name.text = ingredient.Name;
        _description.text = ingredient.Description;
        _count = 0;
        _countText.text = "0";
    }

    public void UpdateCount()
    {
        _count++;
        _countText.text = _count.ToString();
    }

    public void CollectIngredients()
    {
        Debug.Log("Collected");
        _count = 0;
        _countText.text = "0";
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
        Debug.Log("Upgrades");
    }

    public void Water()
    {

    }

    public void Fertilize()
    {

    }

    public void Pests() 
    { 

    }
}
