using UnityEngine;

public abstract class MenuButtonSelector : MonoBehaviour {
    [SerializeField] private MenuButton[] _buttons;

    private MenuButton _nowButton;
    private bool _isWork;

    protected RectTransform _nowRect;
    protected RectTransform _targetRect;

    protected void Awake() {
        for (int i = 0; i < _buttons.Length; i++) {
            _buttons[i].SetIndex(i);
            _buttons[i].ButtonSelected += SelectButton;
        }
        _nowRect = GetComponent<RectTransform>();
    }

    private void Start() {
        SelectButton(0);
    }

    protected virtual void Update() {
        if (!_isWork)
            return;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            SelectButton(_nowButton.Index + 1);
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            SelectButton(_nowButton.Index - 1);
        else if (Input.GetKeyDown(KeyCode.Return))
            PressNowButton();
    }

    private void PressNowButton() {
        if (_nowButton == null)
            return;

        _nowButton.Press();
    }

    private void SelectButton(int index) {
        if (index < 0 || index >= _buttons.Length)
            return;

        _nowButton = _buttons[index];
        _targetRect = _nowButton.Rect;
        ChangePosition();
    }

    public void ChangeState(bool newState) {
        _isWork = newState;
    }

    protected abstract void ChangePosition();
}
