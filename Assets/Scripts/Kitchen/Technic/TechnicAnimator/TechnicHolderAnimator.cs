using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TechnicHolderAnimator : MonoBehaviour {
    protected TechnicHolderData _data;
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void SetData(TechnicHolderData data) {
        _data = data;
    }

    public virtual void UpdateAnimation() {
        _animator.SetBool("isCooking", _data.IsCooking);
    }
}
