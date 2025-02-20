using UnityEngine;

public abstract class MenuButtonSelector : MonoBehaviour {
    [SerializeField] private MenuButton[] _buttons;
    [SerializeField] private AudioSource _audioSource;

    private MenuButton _nowButton;
    protected RectTransform _nowRect;
    protected RectTransform _targetRect;

    protected void Awake() {
        for (int i = 0; i < _buttons.Length; i++) {
            _buttons[i].Setup(i);
            _buttons[i].ButtonSelected += SelectButton;
        }

        _nowRect = GetComponent<RectTransform>();
    }

    private void Start() {
        Invoke(nameof(LateStart), Time.deltaTime);
    }

    private void LateStart() {
        SelectButton(0);
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
