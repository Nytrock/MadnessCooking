using UnityEngine;

namespace MadnessCooking.General {
    public class UIActivator : ColliderActivator {
        [SerializeField] protected ActivableUI _activableObject;
        [SerializeField] private UIActivatorsManager _UIManager;

        protected override void Press() {
            _UIManager.SetActivable(_activableObject);
        }
    }
}
