using System;
using TMPro;
using UnityEngine;

public class CafeOpener : MonoBehaviour, IBindable<CafeData>
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string TextOpened;
    [SerializeField] private string TextClosed;
    private bool _isOpened = true;
    private CafeData _data;

    public bool IsOpened => _isOpened;

    public event Action CafeChanged;

    private void LateStart()
    {
        UpdateCafe();
    }

    public void Bind(CafeData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty) {
            _data.IsOpened = _isOpened;
            LateStart();
            return;
        }

        _isOpened = _data.IsOpened;
        LateStart();
    }

    public void ChangeCafeState()
    {
        _isOpened = !_isOpened;
        _data.IsOpened = _isOpened;
        UpdateCafe();
    }

    private void UpdateCafe()
    {
        if (_isOpened)
            _text.text = TextOpened;
        else
            _text.text = TextClosed;
        CafeChanged?.Invoke();
    }
}
