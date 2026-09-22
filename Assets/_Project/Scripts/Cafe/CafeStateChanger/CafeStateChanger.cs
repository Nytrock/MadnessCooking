using MadnessCooking.General;
using System;
using UnityEngine;

namespace MadnessCooking.Cafe {
    public class CafeStateChanger : MonoBehaviour, ISaveable {
        [SerializeField] private bool _defaultState;

        private CafeStateChangerData _data;

        public bool IsOpened => _data.IsOpened;

        public event Action<bool> CafeChanged;

        public void LateStart() {
            CafeChanged?.Invoke(_data.IsOpened);
        }

        public void LoadSave(GameData data) {
            data.Cafe.CafeOpener ??= new(_defaultState);
            _data = data.Cafe.CafeOpener;
        }

        public void ChangeCafeState() {
            _data.ChangeCafeState();
            CafeChanged?.Invoke(_data.IsOpened);
        }
    }
}
