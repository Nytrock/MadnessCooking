using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {
    [SerializeField] private MusicInfo _mainMenuMusic;
    [SerializeField] private LocationMusicInfo[] _locationMusic;
    [SerializeField] private LocationManager _locationManager;

    private AudioSource _source;
    private bool _isPlaying = true;

    private void Awake() {
        _source = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
            SetMusic(_mainMenuMusic);
        else
            _locationManager.LocationChanged += SetLocationMusic;
    }

    private void SetMusic(MusicInfo music) {
        if (_source.clip == music.Music)
            return;

        _source.volume = music.Volume;
        _source.clip = music.Music;
        _source.Play();
    }

    private void SetLocationMusic(Location location) {
        LocationMusicInfo music = null;
        foreach (var locationMusic in _locationMusic)
            if (locationMusic.Location == location)
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
