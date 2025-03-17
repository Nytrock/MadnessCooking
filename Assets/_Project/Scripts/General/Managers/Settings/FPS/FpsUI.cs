using TMPro;
using UnityEngine;

public class FpsUI : MonoBehaviour {
    [SerializeField] private FpsManager _manager;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake() {
        _manager.FpsShowChanged += ChangeState;
    }

    private void ChangeState(bool newState) {
        _panel.SetActive(newState);
    }

    private void Update() {
        string fps = _manager.GetFPS().ToString("0");
        _text.text = $"{fps} fps";
    }
}
