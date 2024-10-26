using UnityEngine;

public class PestsGeneratorAudio : MonoBehaviour {
    [SerializeField] private PestsGenerator _generator;
    [SerializeField] private AudioSource _cleanAudio;

    private void Awake() {
        _generator.PestsCleaned += _cleanAudio.Play;
    }
}
