using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CafeOpenerAnimator : MonoBehaviour {
    [SerializeField] private CafeOpener _cafeOpener;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _descriptionOpened;
    [SerializeField] private string _descriptionClosed;

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _cafeOpener.CafeChanged += ChangeSignText;
    }

    public void ChangeCafeState() {
        _cafeOpener.ChangeCafeState();
        _animator.SetTrigger("isChanged");
    }

    public void ChangeSignText() {
        _cafeOpener.CafeChanged -= ChangeSignText;
        if (_cafeOpener.IsOpened)
            _text.text = _descriptionOpened;
        else
            _text.text = _descriptionClosed;
    }
}
