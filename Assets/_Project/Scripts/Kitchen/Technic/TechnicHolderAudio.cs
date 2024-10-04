using UnityEngine;

public class TechnicHolderAudio : MonoBehaviour {
    [SerializeField] private TechnicHolder _technicHolder;
    [SerializeField] private AudioSource _repairAudio;
    [SerializeField] private AudioSource _cookAudio;

    private void Awake() {
        _technicHolder.RepairChanged += UpdateRepairAudio;
        _technicHolder.CookChanged += UpdateCookAudio;
    }

    private void UpdateCookAudio() {
        _repairAudio.ChangeState(_technicHolder.Data.IsCooking);
    }

    private void UpdateRepairAudio() {
        _repairAudio.ChangeState(_technicHolder.Data.IsRepairing);
    }
}
