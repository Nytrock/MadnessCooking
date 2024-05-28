using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BedTypeHolder : MonoBehaviour {
    [SerializeField] private BedType _type;
    [SerializeField] private PestsGenerator _pestsGenerator;
    [SerializeField] private FarmBed _farmBed;
    private FarmBedData _bedData;

    private Animator _animator;
    private string _name;
    private float _animationSpeed;

    private StandardBedWater _water;
    private StandardBedFertilize _fertilize;

    public BedType Type => _type;
    public PestsGenerator PestsGenerator => _pestsGenerator;

    private void Awake() {
        _animator = GetComponent<Animator>();
        if (TryGetComponent(out _water))
            _water.BoostEnded += ChangeAnimationSpeed;
        if (TryGetComponent(out _fertilize))
            _fertilize.BoostEnded += ChangeAnimationSpeed;
        _pestsGenerator.PestsChanged += ChangeAnimationSpeed;
        gameObject.SetActive(false);
    }

    private void Update() {
        if (_bedData.PlantedIngredient == null)
            return;

        _bedData.AnimationTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void ChangeMode(bool newMode) {
        gameObject.SetActive(newMode);
        if (!newMode) {
            if (_water != null)
                _water.EndBoost();
            if (_fertilize != null)
                _fertilize.EndBoost();
            _pestsGenerator.ChangeMode(false);
        } else {
            Bind();
        }
    }

    public void SetIngredient() {
        Ingredient ingredient = _bedData.PlantedIngredient;
        _name = ingredient.name;
        _animator.Play(_name, -1, _bedData.AnimationTime);
        StartCoroutine(SetAnimationSpeed(ingredient.TimeGrow));
        _pestsGenerator.ChangeMode(true);
    }

    public void UpdateAnimation() {
        if (!_bedData.IsFull)
            _animator.Play(_name, -1, 0);
    }

    public void StopAnimation() {
        _animator.SetTrigger("disabled");
        _animator.SetFloat("growTime", 1);
    }

    private IEnumerator SetAnimationSpeed(float timeGrow) {
        yield return new WaitForEndOfFrame();
        float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        _animationSpeed = 1 / timeGrow * animationLength;
        _animator.SetFloat("growTime", _animationSpeed * _bedData.SummarizedBoost);
    }

    public void Water() {
        if (_water == null)
            return;

        _water.StartBoost();
        ChangeAnimationSpeed();
    }

    public void ChangeEternalWater() {
        if (_water == null)
            return;

        _water.ChangeEternal();
    }

    public void Fertilize() {
        if (_fertilize == null)
            return;

        _fertilize.StartBoost();
        ChangeAnimationSpeed();
    }

    public void ChangeEternalFertilize() {
        if (_fertilize == null)
            return;

        _fertilize.ChangeEternal();
    }

    public void ChangeAnimationSpeed() {
        _animator.SetFloat("growTime", _animationSpeed * _bedData.SummarizedBoost);
    }

    public void Bind() {
        _bedData = _farmBed.BedData;
        if (_water != null)
            _water.SetData(_bedData.WaterBoost);
        if (_fertilize != null)
            _fertilize.SetData(_bedData.FertilizeBoost);
        _pestsGenerator.SetData(_bedData);
    }
}
