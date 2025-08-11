using System;
using UnityEngine;

public class CafeNameManager : MonoBehaviour, IBindable<CafeData> {
    [SerializeField] private GameSaveManager _saveManager;

    private CafeNameManagerData _data;

    public event Action<string> NameChanged;

    public string CafeName => _data.CafeName;

    public void LateStart() {
        LocalizationManager.Instance.LocalizationChanged += LoadName;
    }

    private void LoadName() {
        LocalizationManager.Instance.LocalizationChanged -= LoadName;
        NameChanged?.Invoke(CafeName);
    }

    public void Bind(CafeData data) {
        data.CafeNameManager ??= new();
        _data = data.CafeNameManager;
    }

    public void ChangeName(string name) {
        _data.ChangeCafeName(name);
        NameChanged?.Invoke(name);
        // _saveManager.Save();
    }
}
