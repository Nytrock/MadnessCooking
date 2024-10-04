using UnityEngine;

public class ClientWalkAudio : PitchableAudioSource {
    [SerializeField] private AudioInfo[] _audios;

    public override void Play() {
        AudioInfo randomAudio = _audios[Random.Range(0, _audios.Length)];
        _audioSource.SetAudioInfo(randomAudio);

        base.Play();
    }
}
