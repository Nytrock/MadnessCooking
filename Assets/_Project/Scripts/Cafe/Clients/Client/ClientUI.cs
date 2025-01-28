using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClientUI : MonoBehaviour, IActivable {
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _chooseFoodPanel;
    [SerializeField] private GameObject _buttonsBlock;
    [SerializeField] private ButtonWithAudio _mainButton;
    [SerializeField] private Image _foodImage;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Slider _eatSlider;

    private CafeUpgradeData _data;
    private ClientData _clientData;
    private UnityAction _startAction;
    private TutorialManager _tutorialManager;
    private UIActivatorsManager _UIManager;

    public event Action<bool> StateChanged;

    public void SetData(CafeUpgradeData data) {
        _data = data;
    }

    public void StartNewCycle() {
        _mainButton.OverrideAllListeners(_startAction);
        _foodImage.color = new Color(1, 1, 1, 0);
    }

    public void StartEat() {
        _eatSlider.maxValue = _clientData.WaitTime;
        ChangeFoodChoiceState(false);
        ChangeSliderState(true);
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
        _foodImage.color = new Color(1, 1, 1, 1);
        _foodImage.sprite = food.Icon;
        _mainButton.OverrideAllListeners(ChangeButtonsBlockVisible);
    }

    public void ActivateYesButton() {
        _animator.SetBool("isFinished", true);
        _yesButton.interactable = true;
    }

    public void ChangeSliderState(bool newValue) {
        _eatSlider.gameObject.SetActive(newValue && _data.IsEatTimeShow);
    }

    public void Setup(ClientData clientData, UnityAction action) {
        _clientData = clientData;
        _startAction = action;

        _eatSlider.maxValue = _clientData.WaitTime;
        _eatSlider.value = _clientData.NowTime;

        _buttonsBlock.SetActive(false);
        _yesButton.interactable = false;
        _animator.SetBool("isFinished", false);
        ChangeSliderState(false);
        ChangeFoodChoiceState(false);
    }

    public void SetupOnCreate(TutorialManager tutorialManager, UIActivatorsManager UIManager) {
        _tutorialManager = tutorialManager;
        _UIManager = UIManager;
    }

    public void UpdateSlider() {
        _eatSlider.value = _clientData.NowTime;
    }
}
