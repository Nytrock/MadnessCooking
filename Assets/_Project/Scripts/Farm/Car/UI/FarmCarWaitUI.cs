using TMPro;
using UnityEngine;

public class FarmCarWaitUI : MonoBehaviour {
    [SerializeField] private FarmCarWaitManager _manager;
    [SerializeField] private LocalizedText _infoText;
    [SerializeField] private string _sentMessage;
    [SerializeField] private string _returnsMessage;
    [SerializeField] private TextMeshProUGUI _timeText;

    private bool _isWait;

    private void Awake() {
        _manager.StateChanged += UpdateState;
    }

    private void UpdateState(CarState newState) {
        _isWait = newState != CarState.Calm;

        if (newState == CarState.Sent)
            _infoText.SetText(_sentMessage);
        else if (newState == CarState.Returns)
            _infoText.SetText(_returnsMessage);
    }

    private void Update() {
        if (_isWait)
            UpdateText();
    }

    private void UpdateText() {
        int nowTime = (int)_manager.NowWaitTime;
        int seconds = nowTime % 60;
        _timeText.text = $"{nowTime / 60}:{seconds:00}";
    }
}
