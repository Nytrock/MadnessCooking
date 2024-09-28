using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {
    [SerializeField] private LocationAudioInfo[] _locationMusic;
    [SerializeField] private LocationManager _locationManager;

    private AudioSource _source;
    private bool _isPlaying = true;

    private void Awake() {
        _source = GetComponent<AudioSource>();
        _locationManager.LocationChanged += SetLocationMusic;
    }

    private void SetMusic(AudioInfo music) {
        if (_source.clip == music.Audio)
            return;

        _source.SetAudioInfo(music);
        _source.Play();
    }

    private void SetLocationMusic(Location location) {
        LocationAudioInfo music = null;
        foreach (var locationMusic in _locationMusic)
            if (locationMusic.ContainsLocation(location))
                music = locationMusic;

        if (music == null)
            return;

        SetMusic(music);
    }

    public void ChangeMusicState() {
        _isPlaying = !_isPlaying;
        if (_isPlaying)
            _source.UnPause();
        else
            _source.Pause();
    }
}
