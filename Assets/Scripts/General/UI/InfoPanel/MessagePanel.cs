using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour {
    [SerializeField] private ContentSizeFitter[] _sizeFitters;
    [SerializeField] private GameObject _panelWithBackground;
    [SerializeField] private GameObject _panel;

    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    [SerializeField] private Button _buttonSubmit;
    [SerializeField] private TextMeshProUGUI _buttonSubmitText;

    private void Awake() {
        ChangeState(false);
        _buttonSubmit.onClick.AddListener(delegate { ChangeState(false); });
    }

    public void SetInfo(MessagePanelInfo info) {
        _titleText.text = info.Title;
        _descriptionText.text = info.Description;
        _panel.transform.position = info.Position;
        _buttonSubmit.gameObject.SetActive(info.IsSubmitButton);
        _buttonSubmitText.text = info.Submit;

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
