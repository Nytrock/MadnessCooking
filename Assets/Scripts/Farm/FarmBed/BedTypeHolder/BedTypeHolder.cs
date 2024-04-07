using System.Collections;
using UnityEngine;

public class BedTypeHolder : MonoBehaviour
{
    [SerializeField] private BedType _type;
    [SerializeField] private PestsGenerator _pestsGenerator;
    [SerializeField] private FarmBed _farmBed;

    private Animator _animator;
    private string _name;
    private int _maxCount;

    private float _animationSpeed;
    private float _boost = 1;

    private StandardBedWater _water;
    private StandardBedFertilize _fertilize;

    public BedType Type => _type;
    public PestsGenerator PestsGenerator => _pestsGenerator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (TryGetComponent(out _water))
            _water.BoostEnded += _farmBed.StopWaterBuff;
        if (TryGetComponent(out _fertilize))
            _fertilize.BoostEnded += _farmBed.StopFertilizeBuff;
        _pestsGenerator.PestsChanged += _farmBed.ChangePestSlowdown;
    }

    public void ChangeMode(bool newMode)
    {
        gameObject.SetActive(newMode);
        if (!newMode) {
            if (_water != null)
                _water.EndBoost();
            if (_fertilize != null)
                _fertilize.EndBoost();
            _pestsGenerator.ChangeMode(false);
        }
    }

    public void SetIngredient(Ingredient ingredient)
    {
        _name = ingredient.name;
        _maxCount = ingredient.MaxCount - 1;
        _animator.Play(_name);
        _animator.SetInteger("leftCount", _maxCount);
        StartCoroutine(SetAnimationSpeed(ingredient.TimeGrow));
        _pestsGenerator.ChangeMode(true);
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
        _animator.SetFloat("growTime", _animationSpeed * _boost);
    }

    public float GetWaterMultiplier()
    {
        if (_water == null)
            return 1;

        var multiplier = _water.StartBoost();
        return multiplier;
    }

    public void ChangeEternalWater()
    {
        if (_water == null)
            return;

        _water.SetEternal(_farmBed.Upgrader.IsWatered);
    }

    public float GetFertilizeMultiptier()
    {
        if (_fertilize == null)
            return 1;

        var multiplier = _fertilize.StartBoost();
        return multiplier;
    }

    public void ChangeEternalFertilize()
    {
        if (_fertilize == null)
            return;

        _fertilize.SetEternal(_farmBed.Upgrader.IsFertilized);
    }

    public void BoostAnimationSpeed(float boost)
    {
        _boost = boost;
        _animator.SetFloat("growTime", _animationSpeed * _boost);
    }
}
