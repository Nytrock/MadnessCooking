using UnityEngine;
using TMPro;

public class FarmCarWaitRenderer : MonoBehaviour
{
    [SerializeField] private FarmCarWaitManager _manager;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _timeText;
    private bool _isActive;

    private void Start()
    {
        _manager.WaitStarted += StartWait;
        _panel.SetActive(false);
    }

    private void StartWait(float waitTime)
    {
        _infoText.text = "Ингредиенты приедут на кухню через:";
        _isActive = true;
        _panel.SetActive(true);
    }

    private void Update()
    {
        if (!_isActive)
            return;

        if (_manager.IsSended) {
            _isActive = false;
            _panel.SetActive(false);
        } else if (_manager.IsWait) {
            _infoText.text = "Машина вернётся через:";
        }

        UpdateText();
    }

    private void UpdateText()
    {
        var nowTime = _manager.NowTime;
        var seconds = (int)nowTime % 60;
        _timeText.text = $"{(int)nowTime / 60}:{seconds:00}";
    }
}
