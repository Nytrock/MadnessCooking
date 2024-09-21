using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ClueTemplate : MonoBehaviour, ITutorialPart {
    [SerializeField] private ClueManager _manager;
    [SerializeField] private string _message;
    [SerializeField] private bool _isScreeenBlocked;
    [SerializeField] private bool _isButtonVisible;
    private RectTransform _rectTransform;

    public RectTransform RectTransform => _rectTransform;
    public string Message => _message;
    public bool IsScreeenBlocked => _isScreeenBlocked;
    public bool IsButtonVisible => _isButtonVisible;

    public event Action PartEnded;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _manager.ClueHided += EndTutorialPart;
    }

    public void StartTutorialPart() {
        _manager.ShowClue(this);
    }

    private void EndTutorialPart() {
        PartEnded?.Invoke();
    }
}
