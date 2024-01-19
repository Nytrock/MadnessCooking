using UnityEngine;

public class GroundBed : MonoBehaviour
{
    [SerializeField] private GroundBedUI _UI;
    private IngredientChoiceUI _ingredientChoice;
    private IngredientType _acceptableType = IngredientType.None;
    private BedHolder _bedHolder;
    private Ingredient _plantedIngredient;
    private Car _car;

    private float _growTime;
    private float _nowTime;
    private int _count;
    private bool _isFull;

    public IngredientType AcceptableType => _acceptableType;
    public BedType BedType => _bedHolder.Type;

    public void MouseDown()
    {
        if (_plantedIngredient == null)
            ActivateIngredientChoice();
        else
            _UI.ChangeMode();
    }

    private void Update()
    {
        if (_isFull || _plantedIngredient == null)
            return;

        if (_nowTime < _growTime) {
            _nowTime += Time.deltaTime;
        } else {
            _count++;
            _UI.UpdateCount();
            _nowTime = 0;
            if (_count == _plantedIngredient.MaxCount) {
                _isFull = true;
                _bedHolder.SetGrow(_isFull);
            }
        }
    }

    public void ActivateIngredientChoice()
    {
        _ingredientChoice.Activate(this);
    }

    public void ResetIngredient()
    {
        _plantedIngredient = null;
        _count = 0;
        _bedHolder.StopAnimation();
    }

    public void SetBedType(BedHolder bedType)
    {
        _bedHolder = bedType;
        _bedHolder.gameObject.SetActive(true);
    }

    public void ResetBedType()
    {
        ResetIngredient();
        _bedHolder.gameObject.SetActive(false);
        _bedHolder = null;
    }

    public void Setup(GroundBedSettings settings)
    {
        _ingredientChoice = settings.IngredientChoiceUI;
        _UI.Setup(settings);
    }

    public void SetIngredient(Ingredient ingredient)
    {
        _plantedIngredient = ingredient;
        _UI.UpdateIngredient(ingredient);
        _bedHolder.SetIngredient(ingredient);
        _growTime = ingredient.TimeGrow;
        _nowTime = 0;
    }

    public void SendIngredients()
    {
        _car.GetIngredients(_count, _plantedIngredient);
        _count = 0;
    }

    public void Water()
    {
        Debug.Log("Water");
    }

    public void Fertilize()
    {
        Debug.Log("Fertilize");
    }
}
