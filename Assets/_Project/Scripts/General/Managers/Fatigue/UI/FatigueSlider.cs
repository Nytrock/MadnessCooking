using UnityEngine;
using UnityEngine.UI;

namespace MadnessCooking.General {
    [RequireComponent(typeof(Slider))]
    public class FatigueSlider : MonoBehaviour {
        [SerializeField] private FatigueManager _manager;
        private Slider _slider;

        private void Awake() {
            _slider = GetComponent<Slider>();
            _slider.maxValue = _manager.FatigueMax;
        }

        private void Update() {
            _slider.value = _slider.maxValue - _manager.FatigueNow;
        }
    }
}
