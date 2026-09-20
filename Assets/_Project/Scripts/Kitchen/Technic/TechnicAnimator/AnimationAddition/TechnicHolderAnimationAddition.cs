using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public abstract class TechnicHolderAnimationAddition : MonoBehaviour {
        public abstract void UpdateAnimation(TechnicHolderData data, bool isTest = false);
    }
}
