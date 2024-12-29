using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour {
    [SerializeField] private string _table;
    protected TextMeshProUGUI _text;

    private string _key;
    private readonly Dictionary<string, string> _arguments = new();

    protected void Awake() {
        GetText();
    }

    protected void Start() {
        UpdateText();
        LocalizationManager.Instance.LocalizationChanged += UpdateText;
    }

    public virtual void SetText(string text) {
        _key = text;
        UpdateText();
    }

    public void SetColor(Color color) {
        GetText();
        _text.color = color;
    }

    public virtual void UpdateText() {
        GetText();
        _text.text = LocalizationManager.Instance.GetLocalization(_table, _key, _arguments);
    }

    private void GetText() {
        if (_text != null)
            return;

        _text = GetComponent<TextMeshProUGUI>();
        if (string.IsNullOrEmpty(_key))
            _key = _text.text;
    }

    public void AddArguments(string name, string variable) {
        _arguments[name] = variable;
    }

    public void ClearArguments() {
        _arguments.Clear();
    }
}
