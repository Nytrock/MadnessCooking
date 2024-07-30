using System;
using UnityEngine;

public class CafeStateChanger : MonoBehaviour, IBindable<CafeData> {
    [SerializeField] private CafeStateChangerData _data;

    public bool IsOpened => _data.IsOpened;

    public event Action CafeChanged;

    private void LateStart() {
        CafeChanged?.Invoke();
    }

    public void Bind(CafeData data) {
        data.CafeOpener ??= new();
        _data = data.CafeOpener;
        LateStart();
    }

    public void ChangeCafeState() {
        _data.ChangeCafeState();
        CafeChanged?.Invoke();
    }
}
