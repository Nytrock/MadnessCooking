using TMPro;
using UnityEngine;

public class FarmCarWaitUI : MonoBehaviour {
    [SerializeField] private FarmCarWaitManager _manager;
    [SerializeField] private GameObject _panel;

    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private string _sentMessage;
    [SerializeField] private string _returnsMessage;

    [SerializeField] private TextMeshProUGUI _timeText;

    private bool _isWait;

    private void Awake() {
        _manager.StateChanged += UpdateState;
    }

    private void Start() {
        _panel.SetActive(false);
    }

    private void UpdateState(CarState newState) {
        _isWait = newState != CarState.Calm;
        _panel.SetActive(_isWait);

        if (newState == CarState.Sent)
            _infoText.text = _sentMessage;
        else if (newState == CarState.Returns)
            _infoText.text = _returnsMessage;
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
