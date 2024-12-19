using System;
using UnityEngine;

public class CafeStateChanger : MonoBehaviour, IBindable<CafeData> {
    private CafeStateChangerData _data;

    public bool IsOpened => _data.IsOpened;

    public event Action<bool> CafeChanged;

    public void LateStart() {
        CafeChanged?.Invoke(_data.IsOpened);
    }

    public void Bind(CafeData data) {
        data.CafeOpener ??= new();
        _data = data.CafeOpener;
    }

    public void ChangeCafeState() {
        _data.ChangeCafeState();
        CafeChanged?.Invoke(_data.IsOpened);
    }
}
