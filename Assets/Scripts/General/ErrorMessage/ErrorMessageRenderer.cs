using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ErrorMessageRenderer : MonoBehaviour {
    [SerializeField] private string _errorMessage;
    [SerializeField] private TextMeshProUGUI _errorText;
    private Animator _errorAnimator;

    private void Awake() {
        _errorAnimator = GetComponent<Animator>();
        _errorText.text = _errorMessage;
    }

    public void ShowError() {
        _errorAnimator.SetTrigger("Error");
    }
}
