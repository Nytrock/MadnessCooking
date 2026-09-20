using UnityEngine;

namespace MadnessCooking.General {
    public class VisualChangersGroup : VisualChanger {
        [SerializeField] private VisualChanger[] _visualChangers;

        protected override void UpdateVisual() {
            foreach (var changer in _visualChangers)
                changer.ChangeState(_isActive);
        }
    }
}
