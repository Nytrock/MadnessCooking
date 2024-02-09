using UnityEngine;
using UnityEngine.UI;

public class OfficeBedUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _sleepButton;

    private void Start()
    {
        _panel.SetActive(false);
    }

    public void ChangeState()
    {
        _panel.SetActive(!_panel.activeSelf);
    }

    public void CheckButton(Daytime daytime)
    {
        _sleepButton.interactable = daytime == Daytime.Night;
    }

    public void StartSleep()
    {
        _panel.SetActive(false);
    }
}
