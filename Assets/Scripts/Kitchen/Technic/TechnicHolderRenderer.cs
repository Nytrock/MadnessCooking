using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TechnicHolderRenderer : MonoBehaviour {
    [SerializeField] private GameObject _standardVisual;
    [SerializeField] private GameObject _repairVisual;
    [SerializeField] private GameObject _brokenVisual;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void UpdateVisual(TechnicHolderData data) {
        _animator.SetBool("isCooking", data.IsCooking);
        _repairVisual.SetActive(data.IsRepairing);
        _standardVisual.SetActive(!data.IsRepairing);
        _brokenVisual.SetActive(data.NowStrength == 0);
    }
}
