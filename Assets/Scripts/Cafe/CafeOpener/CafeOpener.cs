using System;
using TMPro;
using UnityEngine;

public class CafeOpener : MonoBehaviour, IBindable<CafeData> {
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _descriptionOpened;
    [SerializeField] private string _descriptionClosed;
    private CafeData _data;

    public bool IsOpened => _data.IsOpened;

    public event Action CafeChanged;

    private void LateStart() {
        UpdateCafe();
    }

    public void Bind(CafeData data, bool isFileEmpty) {
        _data = data;
        LateStart();
    }

    public void ChangeCafeState() {
        _data.IsOpened = !_data.IsOpened;
        UpdateCafe();
    }

    private void UpdateCafe() {
        if (_data.IsOpened)
            _text.text = _descriptionOpened;
        else
            _text.text = _descriptionClosed;
        CafeChanged?.Invoke();
    }
}
