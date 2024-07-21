using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour {
    [SerializeField] private ContentSizeFitter[] _sizeFitters;
    [SerializeField] private GameObject _panelWithBackground;
    [SerializeField] private GameObject _panel;

    [SerializeField] private LocalizedText _titleText;
    [SerializeField] private LocalizedText _descriptionText;

    [SerializeField] private Button _buttonSubmit;
    [SerializeField] private LocalizedText _buttonSubmitText;

    private void Awake() {
        ChangeState(false);
        _buttonSubmit.onClick.AddListener(delegate { ChangeState(false); });
    }

    public void SetInfo(MessagePanelInfo info) {
        _titleText.SetText(info.Title);
        _descriptionText.SetText(info.Description);
        _panel.transform.position = info.Position;
        _buttonSubmit.gameObject.SetActive(info.IsSubmitButton);
        _buttonSubmitText.SetText(info.Submit);

        StartCoroutine(RefleshPanel());
    }

    private void ChangeState(bool newState) {
        _panelWithBackground.SetActive(newState);
    }

    private IEnumerator RefleshPanel() {
        foreach (var panel in _sizeFitters)
            panel.enabled = false;
        yield return new WaitForEndOfFrame();
        foreach (var panel in _sizeFitters)
            panel.enabled = true;
        ChangeState(true);
    }
}
