using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TechnicHolderAnimator : MonoBehaviour {
    [SerializeField] private TechnicHolder _technicHolder;
    [SerializeField] private TechnicHolderAnimationAddition[] _additions;
    protected Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _technicHolder.CookChanged += UpdateCookAnimation;
    }

    public void UpdateCookAnimation() {
        _animator.SetBool("isCooking", _technicHolder.Data.IsCooking);
        UpdateAdditions();
    }

    [ContextMenu("TestAnimation")]
    public void TestCookAnimation() {
        _animator.SetBool("isCooking", !_animator.GetBool("isCooking"));
        UpdateAdditions(true);
    }

    private void UpdateAdditions(bool isTest = false) {
        foreach (var addition in _additions)
            addition.UpdateAnimation(_technicHolder.Data, isTest);
    }
}
