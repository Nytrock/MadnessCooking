using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour {
    [SerializeField] private string _table;
    private TextMeshProUGUI _text;

    private string _key;
    private Dictionary<string, string> _arguments = new();

    private void Awake() {
        GetText();
    }

    private void Start() {
        UpdateText();
        LocalizationManager.Instance.LocalizationChanged += UpdateText;
    }

    public void SetText(string text) {
        _key = text;
        UpdateText();
    }

    public void SetColor(Color color) {
        if (_text == null)
            GetText();

        _text.color = color;
    }

    public void UpdateText() {
        if (_text == null)
            GetText();

        _text.text = LocalizationManager.Instance.GetLocalization(_table, _key, _arguments);
    }

    private void GetText() {
        _text = GetComponent<TextMeshProUGUI>();
        if (_key == "")
            _key = _text.text;
    }

    public void AddArguments(string name, string variable) {
        _arguments[name] = variable;
    }

    public void ClearArguments() {
        _arguments.Clear();
    }
}
