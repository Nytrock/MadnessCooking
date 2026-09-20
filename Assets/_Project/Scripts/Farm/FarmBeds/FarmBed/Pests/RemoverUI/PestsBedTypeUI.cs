using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class PestsBedTypeUI : MonoBehaviour {
        [SerializeField] private BedType _bedType;

        public BedType BedType => _bedType;

        public void ChangeState(bool value) {
            gameObject.SetActive(value);
        }
    }
}
