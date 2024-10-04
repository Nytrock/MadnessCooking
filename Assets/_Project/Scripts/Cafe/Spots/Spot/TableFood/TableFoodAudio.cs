using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TableFoodAudio : MonoBehaviour {
    [SerializeField] private TableFoodRenderer _renderer;
    [SerializeField] private FoodAudioInfo[] _foodAudioInfo;
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _renderer.StateChanged += ChangeAudio;
    }

    private void ChangeAudio(bool newState) {
        if (!newState)
            return;

        UpdateAudioInfo();
        _audioSource.Play();
    }

    private void UpdateAudioInfo() {
        foreach (var foodAudio in _foodAudioInfo) {
            if (foodAudio.FoodType == _renderer.NowFood.Type) {
                _audioSource.SetAudioInfo(foodAudio);
                return;
            }
        }

        _audioSource.SetAudioInfo(null);
    }
}
