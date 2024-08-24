using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ClueTemplate : TutorialPart {
    [SerializeField] private string _message;
    [SerializeField] private bool _isScreeenBlocked;
    [SerializeField] private bool _isButtonVisible;
    private RectTransform _rectTransform;

    public RectTransform RectTransform => _rectTransform;
    public string Message => _message;
    public bool IsScreeenBlocked => _isScreeenBlocked;
    public bool IsButtonVisible => _isButtonVisible;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
    }
}
