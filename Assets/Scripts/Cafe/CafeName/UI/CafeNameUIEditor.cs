using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CafeNameUIEditor : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _title;
    [SerializeField] private Button _submit;
    [SerializeField] private TMP_InputField _input;
    [SerializeField] private TextMeshProUGUI _charactersCountText;

    public event Action<string> NameEdited;

    private void Awake() {
        _input.onValueChanged.AddListener(CheckName);
    }

    private void CheckName(string name) {
        _charactersCountText.text = $"{name.Length}/{_input.characterLimit}";
        _submit.interactable = name.Length > 0;
    }

    public void StartEditing(string name, bool isTitleEnabled = false) {
        _input.text = name;
        _title.SetActive(isTitleEnabled);
        ChangeState(true);
    }

    public void EndEditing() {
        NameEdited?.Invoke(_input.text);
        ChangeState(false);
    }

    public void ChangeState(bool newState) {
        _panel.SetActive(newState);
    }
}
