using TMPro;
using UnityEngine;

public class OfficeBedUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _blockPanel;
    [SerializeField] private TextMeshProUGUI _sleepButtonText;
    [SerializeField] private string _isSleepText;
    [SerializeField] private string _notSleepText;

    public void LateStart(bool isSleep)
    {
        _panel.SetActive(isSleep);
    }

    public void ChangeState()
    {
        _panel.SetActive(!_panel.activeSelf);
    }

    public void UpdateSleepState(bool isSleep)
    {
        _blockPanel.SetActive(isSleep);
        if (isSleep)
            _sleepButtonText.text = _isSleepText;
        else
            _sleepButtonText.text = _notSleepText;
    }
}
