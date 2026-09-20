using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    [RequireComponent(typeof(Animator))]
    public class CafeStateChangerAnimator : MonoBehaviour {
        [SerializeField] private CafeStateChanger _cafeOpener;
        [SerializeField] private LocalizedText _text;
        [SerializeField] private string _descriptionOpened;
        [SerializeField] private string _descriptionClosed;

        private Animator _animator;

        private void Awake() {
            _animator = GetComponent<Animator>();
            _cafeOpener.CafeChanged += SetSignText;
        }

        public void ChangeCafeState() {
            _cafeOpener.ChangeCafeState();
            _animator.SetTrigger("isChanged");
        }

        private void SetSignText(bool isOpened) {
            _cafeOpener.CafeChanged -= SetSignText;
            ChangeSignText();
        }

        public void ChangeSignText() {
            if (_cafeOpener.IsOpened)
                _text.SetText(_descriptionOpened);
            else
                _text.SetText(_descriptionClosed);
        }
    }
}
