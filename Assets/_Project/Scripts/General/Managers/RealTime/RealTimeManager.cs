using UnityEngine;

namespace MadnessCooking.General {
    public class RealTimeManager : MonoBehaviour, ISaveable {
        [SerializeField] private PauseManager _pauseManager;
        [SerializeField] private RealTimeManagerData _data;

        private void Update() {
            if (_pauseManager.IsPause)
                return;

            _data.UpdateRealTime();
        }

        public void LateStart() { }

        public void LoadSave(GameData data) {
            data.General.RealTimeManager ??= new();
            _data = data.General.RealTimeManager;
        }
    }
}
