using UnityEngine;

public class CriticUIManager : MonoBehaviour
{
    [SerializeField] private Canvas _mainUI;
    [SerializeField] private GameObject _criticCanvas;
    [SerializeField] private GameObject _panelStartWait;
    [SerializeField] private GameObject _panelSuccess;
    [SerializeField] private GameObject _panelFailure;

    private void Start()
    {
        ChangeCriticWaitStartUI(false);
        ChangeCriticWaitSuccessUI(false);
        ChangeCriticWaitFailureUI(false);
    }

    public void ChangeCriticWaitStartUI(bool value)
    {
        _panelStartWait.SetActive(value);
        ChangeUIs(value);
    }

    public void ChangeCriticWaitSuccessUI(bool value)
    {
        _panelSuccess.SetActive(value);
        ChangeUIs(value);
    }

    public void ChangeCriticWaitFailureUI(bool value)
    {
        _panelFailure.SetActive(value);
        ChangeUIs(value);
    }

    private void ChangeUIs(bool value)
    {
        _mainUI.enabled = !value;
        _criticCanvas.SetActive(value);
    }
}
