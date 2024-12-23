using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AutoSaveUI : MonoBehaviour {
    [SerializeField] private AutoSaveManager _manager;
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _manager.SaveChanged += ChangeSaveAnimation;
    }

    public void ChangeSaveAnimation(bool isSaving) {
        _animator.SetBool("isSaving", isSaving);
    }
}
