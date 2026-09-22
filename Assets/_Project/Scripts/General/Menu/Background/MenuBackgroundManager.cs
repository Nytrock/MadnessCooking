using UnityEngine;

namespace MadnessCooking.General {
    public class MenuBackgroundManager : MonoBehaviour {
        [SerializeField] private GameTimeManager _timeManager;
        [SerializeField] private LightManager _lightManager;

        private void Start() {
            GameData stubData = new();
            _lightManager.LoadSave(stubData);
            _timeManager.LoadSave(stubData);

            _lightManager.LateStart();
            _timeManager.LateStart();
        }
    }
}
