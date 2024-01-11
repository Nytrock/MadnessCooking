using System;
using TMPro;
using UnityEngine;

public class CafeOpener : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private bool _isOpened = true;

    public event Action CafeChanged;

    public void ChangeCafeState()
    {
        _isOpened = !_isOpened;
        if (_isOpened)
            text.text = "Open";
        else
            text.text = "Closed";
        CafeChanged?.Invoke();
    }
}
