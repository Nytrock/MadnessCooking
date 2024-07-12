using UnityEngine;

public class InternetManager : MonoBehaviour, IActivable {
    [SerializeField] private Animator _internetAnimator;

    public void ChangeState() {
        _internetAnimator.SetBool("isInternet", !_internetAnimator.GetBool("isInternet"));
    }

    public void ChangeState(bool newState) {
        _internetAnimator.SetBool("isInternet", newState);
    }
}
