using UnityEngine;

public class PestsRemoverUIAudio : MonoBehaviour {
    [SerializeField] private PestsRemoverUI _remover;
    [SerializeField] private AudioSource _activationAudio;

    private void Awake() {
        _remover.RemoverActivated += _activationAudio.Play;
    }
}
