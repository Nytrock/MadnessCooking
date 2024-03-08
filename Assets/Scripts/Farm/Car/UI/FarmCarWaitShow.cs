using UnityEngine;
using TMPro;

public class FarmCarWaitShow : MonoBehaviour
{
    [SerializeField] private FarmCarWaitManager _manager;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _timeText;
    private float _maxTime;
    private float _nowTime;
    private bool _active;
    private bool _isSended;

    private void Start()
    {
        _manager.WaitStarted += StartWait;
        _panel.SetActive(false);
    }

    private void StartWait(float waitTime)
    {
        _maxTime = waitTime;
        _nowTime = _maxTime;
        _infoText.text = "Ингредиенты приедут на кухню через:";
        _active = true;
        _panel.SetActive(true);
    }

    private void Update()
    {
        if (!_active)
            return;

        if (_nowTime > 0) {
            _nowTime -= Time.deltaTime * TimeManager.instance.TimeSpeed;
        } else {
            if (_isSended) {
                _active = false;
                _panel.SetActive(false);
            } else {
                _nowTime = _maxTime;
                _isSended = true;
                _infoText.text = "Машина вернётся через:";
            }
        }

        UpdateText();
    }

    private void UpdateText()
    {
        var seconds = (int)_nowTime % 60;
        if (seconds >= 10)
            _timeText.text = $"{(int) _nowTime / 60}:{seconds}";
        else
            _timeText.text = $"{(int)_nowTime / 60}:0{seconds}";
    }
}
