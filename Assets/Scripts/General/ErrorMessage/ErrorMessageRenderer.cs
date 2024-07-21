using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ErrorMessageRenderer : MonoBehaviour {
    [SerializeField] private string _errorMessage;
    [SerializeField] private LocalizedText _errorText;
    private Animator _errorAnimator;

    private void Awake() {
        _errorAnimator = GetComponent<Animator>();
        _errorText.SetText(_errorMessage);
    }

    public void ShowError() {
        _errorAnimator.SetTrigger("Error");
    }
}
