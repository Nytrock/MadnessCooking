using MadnessCooking.General;
using System;
using UnityEngine;

namespace MadnessCooking.Cafe {
    public class CafeNameManager : MonoBehaviour, ISaveable {
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

        public void LoadSave(GameData data) {
            data.Cafe.CafeNameManager ??= new();
            _data = data.Cafe.CafeNameManager;
        }

        public void ChangeName(string name) {
            _data.ChangeCafeName(name);
            NameChanged?.Invoke(name);
        }
    }
}
