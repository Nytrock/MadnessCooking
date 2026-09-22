using MadnessCooking.General;
using System;
using UnityEngine;

namespace MadnessCooking.Office {
    public class SleepBed : MonoBehaviour, ISaveable {
        [SerializeField] private GameTimeManager _timeManager;
        [SerializeField, Min(0)] private float _sleepTimeSpeed;
        private SleepBedData _data;

        public bool IsSleep => _data.IsSleep;

        public event Action<bool> SleepChanged;

        public void LoadSave(GameData data) {
            data.Office.SleepBed ??= new();
            _data = data.Office.SleepBed;
        }

        public void LateStart() {
            if (_data.IsSleep)
                UpdateSleepState();
        }

        public void ChangeSleepState() {
            _data.ChangeSleepState();
            UpdateSleepState();
        }

        public void ChangeSleepState(bool newState) {
            _data.ChangeSleepState(newState);
            UpdateSleepState();
        }

        private void UpdateSleepState() {
            _timeManager.ChangeTimeSpeed(_data.IsSleep ? _sleepTimeSpeed : _timeManager.DefaultTimeSpeed);
            SleepChanged?.Invoke(_data.IsSleep);
        }

        public float GetSleepBonus(float needHours, float maxFatigue) {
            return maxFatigue / (needHours * 3600 / _sleepTimeSpeed);
        }
    }
}
