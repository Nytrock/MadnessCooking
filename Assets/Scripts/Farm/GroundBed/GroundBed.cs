using UnityEngine;

public class GroundBed : MonoBehaviour
{
    [SerializeField] private GroundBedUI _UI;
    private IngredientChoiceUI _ingredientChoice;
    private IngredientType _acceptableType = IngredientType.None;
    private BedHolder _bedType;
    private Ingredient _plantedIngredient;

    public IngredientType AcceptableType => _acceptableType;
    public BedType BedType => _bedType.Type;

    private void OnMouseDown()
    {
        if (!enabled)
            return;

        if (_plantedIngredient == null)
            _ingredientChoice.Activate(this);
        else
            _UI.ChangeMode();
    }

    public void SetBedType(BedHolder bedType)
    {
        _bedType = bedType;
        _bedType.gameObject.SetActive(true);
    }

    public void SetUI(IngredientChoiceUI ui)
    {
        _ingredientChoice = ui;
    }

    public void SetIngredient(Ingredient ingredient)
    {
        _plantedIngredient = ingredient;
    }
}
