using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClientUI : MonoBehaviour {
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _choseFoodPanel;
    [SerializeField] private GameObject _buttonsBlock;
    [SerializeField] private Button _mainButton;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Slider _waitSlider;
    private Image _foodImage;

    public Slider WaitSlider => _waitSlider;

    private void Awake() {
        _foodImage = _mainButton.GetComponent<Image>();
    }

    public void StartNewCycle(UnityAction action) {
        _mainButton.onClick.RemoveAllListeners();
        _mainButton.onClick.AddListener(action);
        _foodImage.color = new Color(1, 1, 1, 0);
    }

    public void ChangeFoodChoiceState(bool newValue) {
        _choseFoodPanel.SetActive(newValue);
    }

    private void ChangeButtonsBlockVisible() {
        _buttonsBlock.SetActive(!_buttonsBlock.activeSelf);
    }

    public void SetFood(Food food) {
        _foodImage.color = new Color(1, 1, 1, 1);
        _foodImage.sprite = food.Icon;
        _mainButton.onClick.RemoveAllListeners();
        _mainButton.onClick.AddListener(ChangeButtonsBlockVisible);
    }

    public void ActivateYesButton() {
        _animator.SetBool("isFinished", true);
        _yesButton.interactable = true;
    }

    public void ChangeSliderState(bool newValue) {
        _waitSlider.gameObject.SetActive(newValue);
    }

    public void Setup(ClientData clientData) {
        _waitSlider.maxValue = clientData.WaitTime;
        _waitSlider.value = clientData.NowTime;
        _buttonsBlock.SetActive(false);
        _yesButton.interactable = false;
        _animator.SetBool("isFinished", false);
        ChangeSliderState(false);
        ChangeFoodChoiceState(false);
    }
}
