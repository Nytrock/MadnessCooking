using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AutoSaveUI : MonoBehaviour {
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public void PlaySaveAnimation() {
        _animator.SetBool("isSaving", true);
    }

    public void StopSaveAnimation() {
        _animator.SetBool("isSaving", false);
    }
}
