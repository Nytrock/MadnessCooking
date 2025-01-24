using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class AnisToggle : MonoBehaviour {
    [SerializeField] private AnisManager _manager;
    private Toggle _toggle;

    private void Awake() {
        _toggle = GetComponent<Toggle>();
        _toggle.isOn = _manager.IsAnis;
        _toggle.onValueChanged.AddListener(_manager.ChangeAnisState);
    }
}
