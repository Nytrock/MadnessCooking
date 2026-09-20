using System;
using UnityEngine;

namespace MadnessCooking.General {
    [RequireComponent(typeof(RectTransform))]
    public class ClueTemplate : MonoBehaviour, ITutorialPart {
        [SerializeField] private ClueManager _manager;
        [SerializeField] private string _message;
        [SerializeField] private string _buttonMessage;
        [SerializeField] private CluePositionMode _positionMode;
        [SerializeField] private float _positionOffset;
        [SerializeField] private bool _isHoleBlocked;
        [SerializeField] private bool _isButtonVisible;
        private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;
        public string Message => _message;
        public bool IsHoleBlocked => _isHoleBlocked;
        public bool IsButtonVisible => _isButtonVisible;
        public string ButtonMessage => _buttonMessage;
        public CluePositionMode PositionMode => _positionMode;
        public float PositionOffset => _positionOffset;

        public event Action PartEnded;

        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void StartTutorialPart() {
            _manager.ShowClue(this);
            _manager.ClueHided += EndTutorialPart;
        }

        private void EndTutorialPart() {
            _manager.ClueHided -= EndTutorialPart;
            PartEnded?.Invoke();
        }
    }
}
