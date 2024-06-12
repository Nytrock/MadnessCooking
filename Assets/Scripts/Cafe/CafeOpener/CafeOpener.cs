using System;
using UnityEngine;

public class CafeOpener : MonoBehaviour, IBindable<CafeData> {
    private CafeData _data;

    public bool IsOpened => _data.IsOpened;

    public event Action CafeChanged;

    private void LateStart() {
        CafeChanged?.Invoke();
    }

    public void Bind(CafeData data, bool isFileEmpty) {
        _data = data;
        LateStart();
    }

    public void ChangeCafeState() {
        _data.IsOpened = !_data.IsOpened;
        CafeChanged?.Invoke();
    }
}
