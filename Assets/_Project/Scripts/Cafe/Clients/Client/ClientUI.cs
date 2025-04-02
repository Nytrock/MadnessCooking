using System;
using UnityEngine;
using UnityEngine.UI;

public class ClientUI : MonoBehaviour, IActivable {
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _chooseFoodPanel;
    [SerializeField] private GameObject _buttonsBlock;
    [SerializeField] private ButtonWithAudio _mainButton;
    [SerializeField] private Image _foodImage;
    [SerializeField] private Sprite _questionSprite;
    [SerializeField] private CircleSlider _orderSlider;
    [SerializeField] private Slider _eatSlider;

    private Client _client;
    private CafeUpgradeData _upgradeData;
    private TutorialManager _tutorialManager;
    private UIActivatorsManager _UIManager;

    public event Action<bool> StateChanged;

    private void Awake() {
        _mainButton.onClick.AddListener(OnMainButtonClicked);
    }

    private void OnMainButtonClicked() {
        if (!_client.Data.Order.IsActivated) {
            _client.ActivateOrder();
            return;
        }

        if (_client.Data.Order.IsFinished) {
            _client.Eat();
            return;
        }

        ChangeButtonsBlockVisible();
    }

    public void StartNewCycle() {
        _foodImage.sprite = _questionSprite;
        ChangeFoodChoiceState(false);
    }

    public void StartEat() {
        _eatSlider.maxValue = _client.Data.WaitTime;
        ChangeFoodChoiceState(false);
        ChangeEatSliderState(true);
    }

    private void Update() {
        if (_client.Data.State == ClientState.Eat)
            _eatSlider.value = _client.Data.NowTime;
        else if (_client.Data.Order.IsCooking)
            _orderSlider.SetValue(_client.Data.Order.CookProgress);
    }

    public void ChangeFoodChoiceState(bool newValue) {
        _chooseFoodPanel.SetActive(newValue);
    }

    public void ChangeState(bool newState) {
        if (_tutorialManager.IsWork)
            _tutorialManager.NextTutorialPart();

        _buttonsBlock.SetActive(newState);
        StateChanged?.Invoke(newState);
    }

    private void ChangeButtonsBlockVisible() {
        _UIManager.SetActivable(this);
    }

    public void SetFood(Food food) {
        _foodImage.sprite = food.Icon;
    }

    public void FinishOrder() {
        _animator.SetBool("isFinished", true);
        _buttonsBlock.SetActive(false);
        _orderSlider.SetValue(0);
    }

    public void ChangeEatSliderState(bool newValue) {
        _eatSlider.gameObject.SetActive(newValue && _upgradeData.IsEatTimeShow);
    }

    public void SetupOnCreate(Client client, TutorialManager tutorialManager, UIActivatorsManager UIManager, CafeUpgradeData upgradeData) {
        _client = client;
        _tutorialManager = tutorialManager;
        _UIManager = UIManager;
        _upgradeData = upgradeData;

        _buttonsBlock.SetActive(false);
        _animator.SetBool("isFinished", false);
        ChangeEatSliderState(false);
    }
}
