using System;
using UnityEngine;

public class CafeNameManager : MonoBehaviour, IBindable<CafeData> {
    private CafeNameManagerData _data;

    public event Action<string> NameChanged;

    public string CafeName => _data.CafeName;

    private void LateStart() {
        NameChanged?.Invoke(_data.CafeName);
    }

    public void Bind(CafeData data) {
        data.CafeNameManager ??= new();
        _data = data.CafeNameManager;

        LateStart();
    }

    public void ChangeName(string name) {
        _data.ChangeCafeName(name);
        NameChanged?.Invoke(name);
    }
}
