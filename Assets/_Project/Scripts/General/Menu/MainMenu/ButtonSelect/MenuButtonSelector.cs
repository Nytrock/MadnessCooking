using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class MenuButtonSelector : MonoBehaviour {
    [SerializeField] private MenuButton[] _buttons;

    private AudioSource _audioSource;
    private MenuButton _nowButton;

    protected RectTransform _nowRect;
    protected RectTransform _targetRect;

    protected void Awake() {
        for (int i = 0; i < _buttons.Length; i++) {
            _buttons[i].SetIndex(i);
            _buttons[i].ButtonSelected += SelectButton;
        }

        _nowRect = GetComponent<RectTransform>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        Invoke(nameof(LateStart), Time.deltaTime);
    }

    private void LateStart() {
        SelectButton(0);
    }

    protected virtual void Update() {
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

        if (_buttons[index] == _nowButton)
            return;

        _audioSource.Play();
        _nowButton = _buttons[index];
        _targetRect = _nowButton.Rect;
        ChangePosition();
    }

    protected abstract void ChangePosition();
}
