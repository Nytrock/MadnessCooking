using TMPro;
using UnityEngine;

public class FarmCarWaitUI : MonoBehaviour
{
    [SerializeField] private FarmCarWaitManager _manager;
    [SerializeField] private GameObject _panel;

    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private string _sentText;
    [SerializeField] private string _returnsText;

    [SerializeField] private TextMeshProUGUI _timeText;

    private void Start()
    {
        _panel.SetActive(false);
    }

    private void Update()
    {
        bool isCarCalm = _manager.Data.CarState == CarState.Calm;
        _panel.SetActive(!isCarCalm);
        if (isCarCalm)
            return;

        if (_manager.Data.CarState == CarState.Sent)
            _infoText.text = _sentText;
        else if (_manager.Data.CarState == CarState.Returns)
            _infoText.text = _returnsText;

        UpdateText();
    }

    private void UpdateText()
    {
        int nowTime = (int)_manager.Data.NowWaitTime;
        var seconds = nowTime % 60;
        _timeText.text = $"{nowTime / 60}:{seconds:00}";
    }
}
