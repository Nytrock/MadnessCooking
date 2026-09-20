using UnityEngine;

namespace MadnessCooking.General {
    public class PlatformSpecificObject : MonoBehaviour {
        [SerializeField] private bool _forDesktop;
        [SerializeField] private bool _forMobile;
        [SerializeField] private bool _forWeb;

        private void Awake() {
            gameObject.SetActive(GetState());
        }

        private bool GetState() {
            if (!_forMobile && PlatformManager.IsMobile)
                return false;

            if (!_forWeb && PlatformManager.IsWeb)
                return false;

            if (!_forDesktop && !PlatformManager.IsMobile && !PlatformManager.IsWeb)
                return false;

            return true;
        }
    }
}
