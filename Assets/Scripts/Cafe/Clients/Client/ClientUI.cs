using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClientUI : MonoBehaviour {
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _chooseFoodPanel;
    [SerializeField] private GameObject _buttonsBlock;
    [SerializeField] private Button _mainButton;
    [SerializeField] private Image _foodImage;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Slider _eatSlider;

    private CafeUpgradeData _data;
    private ClientData _clientData;

    public void SetData(CafeUpgradeData data) {
        _data = data;
    }

    public void StartNewCycle(UnityAction action) {
        _mainButton.OverrideAllListeners(action);
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

    private void ChangeButtonsBlockVisible() {
        _buttonsBlock.SetActive(!_buttonsBlock.activeSelf);
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

    public void Setup(ClientData clientData) {
        _clientData = clientData;
        _eatSlider.maxValue = _clientData.WaitTime;
        _eatSlider.value = _clientData.NowTime;

        _buttonsBlock.SetActive(false);
        _yesButton.interactable = false;
        _animator.SetBool("isFinished", false);
        ChangeSliderState(false);
        ChangeFoodChoiceState(false);
    }

    public void UpdateSlider() {
        _eatSlider.value = _clientData.NowTime;
    }
}
