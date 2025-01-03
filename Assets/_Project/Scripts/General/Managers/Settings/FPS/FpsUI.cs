using TMPro;
using UnityEngine;

public class FpsUI : MonoBehaviour {
    [SerializeField] private FpsManager _manager;
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake() {
        _manager.FpsShowChanged += ChangeState;
    }

    private void ChangeState(bool newState) {
        gameObject.SetActive(newState);
    }

    private void Update() {
        _text.text = $"{_manager.GetFPS():F0} fps";
    }
}
