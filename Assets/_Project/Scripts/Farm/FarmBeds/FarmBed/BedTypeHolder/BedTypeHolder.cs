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
        CheckBoosts();

        if (_water != null)
            _water.BoostEnded += UpdateAnimationSpeed;
        if (_fertilize != null)
            _fertilize.BoostEnded += UpdateAnimationSpeed;
        _pestsGenerator.PestsChanged += UpdateAnimationSpeed;
    }

    private void CheckBoosts() {
        if (_water == null)
            TryGetComponent(out _water);
        if (_fertilize == null)
            TryGetComponent(out _fertilize);
    }

    private void Update() {
        if (_bedData.PlantedIngredient == null)
            return;

        float animationTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        _bedData.SetAnimationTime(Mathf.Max(animationTime, 0.02f));
    }

    public void ChangeState(bool newMode) {
        gameObject.SetActive(newMode);

        if (newMode)
            ActivateBoostsAndPests();
    }

    private void ActivateBoostsAndPests() {
        if (_water != null)
            _water.Activate();
        if (_fertilize != null)
            _fertilize.Activate();
        _pestsGenerator.Activate();
    }

    public void SetIngredient() {
        Ingredient ingredient = _bedData.PlantedIngredient;
        _name = ingredient.name;

        _pestsGenerator.ChangeState(true);
        StartCoroutine(SetAnimationSpeed(ingredient.TimeGrow));
        _animator.Play(_name, -1, _bedData.AnimationTime);
    }

    public void UpdateAnimation() {
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
        UpdateAnimationSpeed();
    }

    public void UpdateEternalWater() {
        if (_water == null)
            return;

        _water.UpdateEternal();
        UpdateAnimationSpeed();
    }

    public void Fertilize() {
        if (_fertilize == null)
            return;

        _fertilize.StartBoost();
        UpdateAnimationSpeed();
    }

    public void UpdateEternalFertilize() {
        if (_fertilize == null)
            return;

        _fertilize.UpdateEternal();
        UpdateAnimationSpeed();
    }

    public void UpdateAnimationSpeed() {
        _animator.SetFloat("growTime", _animationSpeed * _bedData.SummarizedBoost);
    }

    public void Bind(FarmBedData bedData) {
        _bedData = bedData;
        CheckBoosts();

        if (_water != null)
            _water.Bind(_bedData.WaterBoost);
        if (_fertilize != null)
            _fertilize.Bind(_bedData.FertilizeBoost);
        _pestsGenerator.Bind(_bedData.PestsGenerator);
    }
}
