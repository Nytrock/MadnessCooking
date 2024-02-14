using System.Collections;
using UnityEngine;

public class BedTypeHolder : MonoBehaviour
{
    [SerializeField] private BedType _type;
    [SerializeField] private GroundBed _groundBed;

    private Animator _animator;
    private string _name;
    private int _maxCount;

    private float _animationSpeed;

    private StandardBedWater _water;
    private StandardBedFertilize _fertilize;

    public BedType Type => _type;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (TryGetComponent(out _water))
            _water.BoostEnded += _groundBed.StopWaterBuff;
        if (TryGetComponent(out _fertilize))
            _fertilize.BoostEnded += _groundBed.StopFertilizeBuff;
    }

    public void ChangeMode(bool newMode)
    {
        gameObject.SetActive(newMode);
    }

    public void SetIngredient(Ingredient ingredient)
    {
        _name = ingredient.name;
        _maxCount = ingredient.MaxCount - 1;
        _animator.Play(_name);
        _animator.SetInteger("leftCount", _maxCount);
        StartCoroutine(SetAnimationSpeed(ingredient.TimeGrow));
    }

    public void ResetAnimation(bool isFull)
    {
        if (_animator.GetInteger("leftCount") == 0 && isFull) {
            _animator.Play(_name, -1, 0);
            _animator.SetInteger("leftCount", _maxCount + 1);
        } else {
            _animator.SetInteger("leftCount", _maxCount);
        }
    }

    public void StopAnimation()
    {
        _animator.SetTrigger("disabled");
        _animator.SetFloat("growTime", 1);
    }

    private IEnumerator SetAnimationSpeed(float timeGrow)
    {
        yield return new WaitForEndOfFrame();
        var animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        _animationSpeed = 1 / timeGrow * animationLength;
        _animator.SetFloat("growTime", _animationSpeed);
    }

    public float GetWaterMultiplier()
    {
        if (_water == null)
            return 1;

        var multiplier = _water.StartBoost();
        BoostAnimationSpeed(multiplier);
        return multiplier;
    }

    public float GetFertilizeMultiptier()
    {
        if (_fertilize == null)
            return 1;

        var multiplier = _fertilize.StartBoost();
        BoostAnimationSpeed(multiplier);
        return multiplier;
    }

    public void BoostAnimationSpeed(float boost)
    {
        _animator.SetFloat("growTime", _animationSpeed * boost);
    }
}
