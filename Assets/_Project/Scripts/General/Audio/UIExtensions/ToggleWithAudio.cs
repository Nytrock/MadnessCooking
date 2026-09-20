using UnityEngine;
using UnityEngine.UI;

namespace MadnessCooking.General {
    public class ToggleWithAudio : Toggle {
        [SerializeField] private AudioSource _audioSource;

        protected override void Awake() {
            base.Awake();

            if (_audioSource == null)
                TryGetComponent(out _audioSource);
            onValueChanged.AddListener(delegate { _audioSource.Play(); });
        }
    }
}
