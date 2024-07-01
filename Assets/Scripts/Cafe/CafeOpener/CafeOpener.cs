using System;
using UnityEngine;

public class CafeOpener : MonoBehaviour, IBindable<CafeData> {
    private CafeOpenerData _data;

    public bool IsOpened => _data.IsOpened;

    public event Action CafeChanged;

    private void LateStart() {
        CafeChanged?.Invoke();
    }

    public void Bind(CafeData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.CafeOpener = new();
        _data = data.CafeOpener;
        LateStart();
    }

    public void ChangeCafeState() {
        _data.ChangeCafeState();
        CafeChanged?.Invoke();
    }
}
