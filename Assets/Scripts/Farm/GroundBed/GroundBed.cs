using UnityEngine;

public class GroundBed : MonoBehaviour
{
    [SerializeField] private GroundBedUIManager _UI;
    private IngredientChoiceUI _ingredientChoice;
    private IngredientType _acceptableType = IngredientType.None;
    private BedTypeHolder _bedHolder;
    private Ingredient _plantedIngredient;
    private Car _car;

    private float _growTime;
    private float _nowTime;
    private int _count;
    private bool _isFull;

    [SerializeField] private float _waterBoost = 1;
    [SerializeField] private float _fertilizeBoost = 1;

    public IngredientType AcceptableType => _acceptableType;
    public BedType BedType => _bedHolder.Type;
    public int Count => _count;

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
            _nowTime += Time.deltaTime * _waterBoost * _fertilizeBoost;
        } else {
            _count++;
            _UI.UpdateCount();
            _nowTime = 0;
            if (_count == _plantedIngredient.MaxCount)
                _isFull = true;
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
        _isFull = false;
        _bedHolder.StopAnimation();
    }

    public void SetBedType(BedTypeHolder bedType)
    {
        if (bedType.Type.AcceptableType == IngredientType.Water)
            _waterBoost = 0;
        else
            _waterBoost = 1;

        _bedHolder = bedType;
        _bedHolder.ChangeMode(true);
    }

    public void ResetBedType()
    {
        ResetIngredient();
        _bedHolder.ChangeMode(false);
        _bedHolder = null;
    }

    public void Setup(GroundBedSettings settings)
    {
        _ingredientChoice = settings.IngredientChoiceUI;
        _car = settings.Car;
        _UI.Setup(settings);
    }

    public void SetIngredient(Ingredient ingredient)
    {
        _plantedIngredient = ingredient;
        _UI.UpdateIngredient(ingredient, _bedHolder);
        _bedHolder.SetIngredient(ingredient);
        _growTime = ingredient.TimeGrow;
        _nowTime = 0;
    }

    public void SendIngredients()
    {
        if (_count == 0)
            return;

        var carSpace = _car.GetSpace();
        if (carSpace == 0) {
            return;
        }

        if (carSpace < _count) {
            _car.PutIngredient(carSpace, _plantedIngredient);
            _count -= carSpace;
        } else {
            _car.PutIngredient(_count, _plantedIngredient);
            _count = 0;
        }

        _UI.UpdateCount();
        _bedHolder.ResetAnimation(_isFull);
        _isFull = false;
    }

    public void Water()
    {
        _waterBoost = _bedHolder.GetWaterMultiplier();
    }

    public void Fertilize()
    {
        _fertilizeBoost = _bedHolder.GetFertilizeMultiptier();
    }

    public void StopWaterBuff(float newMultiplier)
    {
        _waterBoost = newMultiplier;
    }

    public void StopFertilizeBuff(float newMultiplier)
    {
        _fertilizeBoost = newMultiplier;
    }
}
