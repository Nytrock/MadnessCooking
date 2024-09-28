using TMPro;
using UnityEngine;

public class InputWithAudio : TMP_InputField {
    [SerializeField] private AudioSource _audioSource;

    protected override void Awake() {
        base.Awake();

        if (_audioSource == null)
            TryGetComponent(out _audioSource);
        onValueChanged.AddListener(delegate { _audioSource.Play(); });
    }
}
