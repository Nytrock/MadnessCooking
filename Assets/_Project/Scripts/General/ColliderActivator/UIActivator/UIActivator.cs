using AYellowpaper;
using UnityEngine;

namespace MadnessCooking.General {
    public class UIActivator : ColliderActivator {
        [SerializeField] protected InterfaceReference<IStateable> _activableObject;
        [SerializeField] private UIActivatorsManager _UIManager;

        protected override void Press() {
            _UIManager.SetActivable(_activableObject.Value);
        }
    }
}
