using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWithAudio : Button {
    [SerializeField] private AudioSource _audioSource;

    protected override void Awake() {
        base.Awake();

        if (_audioSource == null)
            TryGetComponent(out _audioSource);
    }

    public override void OnPointerClick(PointerEventData eventData) {
        if (interactable)
            _audioSource.Play();
        base.OnPointerClick(eventData);
    }

    public void ForceSoundPlay() {
        _audioSource.Play();
    }
}
