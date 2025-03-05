using UnityEngine;

public class ClientWalkAudio : PitchableAudioSource {
    [SerializeField] private AudioInfo[] _audios;

    public override void Play() {
        AudioInfo randomAudio = _audios.GetRandom();
        _audioSource.SetAudioInfo(randomAudio);

        base.Play();
    }
}
