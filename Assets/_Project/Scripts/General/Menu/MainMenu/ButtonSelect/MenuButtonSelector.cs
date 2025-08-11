using System.Collections.Generic;
using UnityEngine;

public abstract class MenuButtonSelector : MonoBehaviour {
    [SerializeField] private MenuButton[] _buttons;
    [SerializeField] private AudioSource _audioSource;

    private MenuButton _nowButton;
    protected RectTransform _nowRect;
    protected RectTransform _targetRect;

    protected void Awake() {
        if (!Application.isMobilePlatform)
            LocalizationManager.Instance.LocalizationChanged += SelectFirstButton;
        CheckButtons();
    }

    private void CheckButtons() {
        List<MenuButton> newButtons = new();
        foreach (var button in _buttons)
            if (button.gameObject.activeSelf)
                newButtons.Add(button);
        _buttons = newButtons.ToArray();
    }

    protected void Start() {
        if (Application.isMobilePlatform) {
            MobileStart();
            return;
        }

        StandardStart();
    }

    private void StandardStart() {
        for (int i = 0; i < _buttons.Length; i++) {
            _buttons[i].Setup(i);
            _buttons[i].ButtonSelected += SelectButton;
        }

        _nowRect = GetComponent<RectTransform>();
    }

    private void MobileStart() {
        foreach (var button in _buttons)
            button.ChangeSelectVisual(true);
        gameObject.SetActive(false);
    }

    protected virtual void Update() {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            SelectButton(_nowButton.Index + 1);
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            SelectButton(_nowButton.Index - 1);
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            PressNowButton();
    }

    private void PressNowButton() {
        if (_nowButton == null)
            return;

        _nowButton.Press();
    }

    public void SelectFirstButton() {
        SelectButton(0);
        LocalizationManager.Instance.LocalizationChanged -= SelectFirstButton;
    }

    private void SelectButton(int index) {
        if (index < 0)
            index = _buttons.Length - 1;
        else if (index >= _buttons.Length)
            index = 0;

        if (_buttons[index] == _nowButton)
            return;

        if (_audioSource.isActiveAndEnabled)
            _audioSource.Play();

        if (_nowButton != null)
            _nowButton.ChangeSelectVisual(false);
        _nowButton = _buttons[index];
        _nowButton.ChangeSelectVisual(true);

        _targetRect = _nowButton.Rect;
        ChangePosition();
    }

    protected abstract void ChangePosition();
}
