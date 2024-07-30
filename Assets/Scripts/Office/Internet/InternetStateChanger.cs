using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class InternetStateChanger : MonoBehaviour, IActivable {
    private const string ANIMATION_NAME = "isInternet";

    [SerializeField] private Animator _cameraAnimator;
    private CanvasGroup _canvasGroup;

    private void Awake() {
        ChangeGroupState(false);
    }

    public void ChangeState() {
        _cameraAnimator.SetBool(ANIMATION_NAME, !_cameraAnimator.GetBool(ANIMATION_NAME));
        ChangeGroupState(_cameraAnimator.GetBool(ANIMATION_NAME));
    }

    public void ChangeState(bool newState) {
        _cameraAnimator.SetBool(ANIMATION_NAME, newState);
        ChangeGroupState(newState);
    }

    private void ChangeGroupState(bool newState) {
        if (_canvasGroup == null)
            _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.interactable = newState;
        _canvasGroup.blocksRaycasts = newState;
    }
}
