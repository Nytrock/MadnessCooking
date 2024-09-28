using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderWithAudio : Slider {
    [SerializeField] private AudioSource _audioSource;

    protected override void Awake() {
        base.Awake();

        if (_audioSource == null)
            TryGetComponent(out _audioSource);
    }

    public override void OnPointerDown(PointerEventData eventData) {
        _audioSource.Play();
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData) {
        _audioSource.Play();
        base.OnPointerUp(eventData);
    }
}
