using System.Collections;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    [RequireComponent(typeof(Animator))]
    public class BedTypeHolder : MonoBehaviour {
        [SerializeField] private BedType _type;
        [SerializeField] private PestsGenerator _pestsGenerator;
        [SerializeField] private FarmBed _farmBed;
        [SerializeField] private BedHolderBooster _water;
        [SerializeField] private BedHolderBooster _fertilize;
        [SerializeField] private GameObject _intependentBooster;

        private FarmBedData _bedData;
        private Animator _animator;
        private string _name;
        private float _animationSpeed;

        public BedType Type => _type;
        public PestsGenerator PestsGenerator => _pestsGenerator;

        private void Awake() {
            _animator = GetComponent<Animator>();

            if (_water != null)
                _water.BoostEnded += UpdateAnimationSpeed;
            if (_fertilize != null)
                _fertilize.BoostEnded += UpdateAnimationSpeed;
            _pestsGenerator.PestsChanged += UpdateAnimationSpeed;
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
            UpdateIntependentBooster();
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
            StartBoost(_water);
        }

        public void Fertilize() {
            StartBoost(_fertilize);
        }

        private void StartBoost(BedHolderBooster booster) {
            if (booster == null)
                return;

            booster.StartBoost();
            UpdateAnimationSpeed();
        }

        public void UpdateUpgrades() {
            UpdateEternalStatusOnBooster(_water);
            UpdateEternalStatusOnBooster(_fertilize);
            UpdateIntependentBooster();
            UpdateAnimationSpeed();
        }

        private void UpdateIntependentBooster() {
            if (_intependentBooster == null)
                return;

            _intependentBooster.SetActive(_bedData.IndependentBoost != 1);
        }

        public void UpdateEternalStatusOnBooster(BedHolderBooster booster) {
            if (booster == null)
                return;

            booster.UpdateEternal();
        }

        public void UpdateAnimationSpeed() {
            _animator.SetFloat("growTime", _animationSpeed * _bedData.SummarizedBoost);
        }

        public void Bind(FarmBedData bedData) {
            _bedData = bedData;

            if (_water != null)
                _water.Bind(_bedData.WaterBoost);
            if (_fertilize != null)
                _fertilize.Bind(_bedData.FertilizeBoost);
            _pestsGenerator.Bind(_bedData.PestsGenerator);
        }
    }
}
