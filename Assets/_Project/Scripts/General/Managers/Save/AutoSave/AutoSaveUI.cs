using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AutoSaveUI : MonoBehaviour {
    [SerializeField] private AutoSaveManager _manager;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _manager.SaveStarted += StartSaveAnimation;
    }

    public void StartSaveAnimation() {
        _animator.SetTrigger("isSaving");
    }
}
