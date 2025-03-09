using UnityEngine;

public class ClientMainAudio : MonoBehaviour {
    [SerializeField] private Client _client;
    [SerializeField] private AudioSource _orderAudio;
    [SerializeField] private AudioSource _rejectAudio;
    [SerializeField] private AudioSource _eatAudio;
    private float _orderAudioOriginalVolume;

    private void Awake() {
        _client.SetupEnded += SetupAudioSources;
        _orderAudioOriginalVolume = _orderAudio.volume;
    }

    private void SetupAudioSources() {
        _orderAudio.volume = _orderAudioOriginalVolume / _client.Holder.ClientsCount;

        _client.OrderActivated += delegate { _orderAudio.Play(); };
        _client.ClientRejected += delegate { _rejectAudio.Play(); };
        _client.ClientEat += delegate { _eatAudio.Play(); };
    }
}
