using System;
using UnityEngine;

namespace MadnessCooking.General {
    public class AutoSaveManager : MonoBehaviour {
        [SerializeField, Min(1)] private int _needAutoSaveMinutes;
        [SerializeField] private GameSaveManager _saveManager;
        [SerializeField] private TutorialManager _tutorialManager;

        [SerializeField] private float _nowTime;
        [SerializeField] private float _needAutoSaveTime;

        public event Action SaveStarted;

        private void Awake() {
            _needAutoSaveTime = 60 * _needAutoSaveMinutes;
        }

        private void Update() {
            if (_tutorialManager.IsWork)
                return;

            if (_nowTime < _needAutoSaveTime)
                _nowTime += Time.unscaledDeltaTime;
            else if (!FatigueManager.Instance.IsTired)
                StartAutoSave();
        }

        private void StartAutoSave() {
            _nowTime = 0;
            SaveStarted?.Invoke();
            _saveManager.Save();
        }
    }
}