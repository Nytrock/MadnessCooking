using UnityEngine;

namespace MadnessCooking.General {
    public class UIActivatorsManager : MonoBehaviour {
        [SerializeField] private LocationManager _locationManager;
        private ActivableUI _nowActivable;

        private void Awake() {
            _locationManager.LocationChanged += delegate { CloseNowActivable(); };
        }

        public void CloseNowActivable() {
            if (_nowActivable != null) {
                _nowActivable.ChangeState(false);
                _nowActivable = null;
            }
        }

        public void SetActivable(ActivableUI activable) {
            if (activable == _nowActivable) {
                _nowActivable.ChangeState(false);
                _nowActivable = null;
                return;
            }

            CloseNowActivable();
            _nowActivable = activable;
            _nowActivable.ChangeState(true);
        }
    }
}
