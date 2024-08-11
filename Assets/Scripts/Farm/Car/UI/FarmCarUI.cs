using UnityEngine;
using UnityEngine.UI;

public class FarmCarUI : IngredientStorageUI<FarmData> {
    [SerializeField] private FarmCarWaitManager _waitManager;
    [SerializeField] private Button _sendButton;

    protected override void Awake() {
        if (_sendButton != null) {
            _sendButton.onClick.AddListener(_waitManager.StartWait);
            _sendButton.onClick.AddListener(CarLeave);
        }

        _waitManager.StateChanged += UpdateState;
        base.Awake();
    }

    private void UpdateState(CarState newState) {
        if (newState != CarState.Returns)
            ChangeState(false);

        if (_sendButton != null)
            _sendButton.interactable = newState == CarState.Calm;
    }

    private void CarLeave() {
        foreach (var button in _buttons)
            _buttonPool.PutObject(button);
        _buttons.Clear();
    }
}
