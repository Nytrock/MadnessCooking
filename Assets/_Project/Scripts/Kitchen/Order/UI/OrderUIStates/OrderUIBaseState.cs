using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public class OrderUIBaseState : MonoBehaviour {
        [SerializeField] protected OrderUIState _state;

        public virtual void UpdateState(OrderUIState newState) {
            gameObject.SetActive(newState == _state);
        }
    }
}
