using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FpsUI : MonoBehaviour {
    [SerializeField] private FpsManager _manager;
    private TextMeshProUGUI _text;


    private void Awake() {
        _text = GetComponent<TextMeshProUGUI>();
        _manager.FpsShowChanged += ChangeState;
    }

    private void ChangeState(bool newState) {
        gameObject.SetActive(newState);
    }

    private void Update() {
        _text.text = $"{_manager.GetFPS():F0} fps";
    }
}
