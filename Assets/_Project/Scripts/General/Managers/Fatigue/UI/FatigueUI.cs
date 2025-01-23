using UnityEngine;

public class FatigueUI : MonoBehaviour {
    [SerializeField] private FatigueManager _manager;
    [SerializeField] private Animator _screenAnimator;

    private void Awake() {
        _manager.TiredChanged += ChangeTiredAnimation;
    }

    private void ChangeTiredAnimation(bool newState) {
        _screenAnimator.SetBool("isTired", newState);
    }
}
