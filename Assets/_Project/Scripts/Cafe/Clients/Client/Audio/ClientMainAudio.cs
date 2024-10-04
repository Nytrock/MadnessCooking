using UnityEngine;

public class ClientMainAudio : MonoBehaviour {
    [SerializeField] private Client _client;
    [SerializeField] private AudioSource _orderAudio;
    [SerializeField] private AudioSource _rejectAudio;
    [SerializeField] private AudioSource _eatAudio;

    private void Awake() {
        _client.ClientSetup += SetupAudioSources;
    }

    private void SetupAudioSources() {
        _client.OrderActivated += delegate { _orderAudio.Play(); };
        _client.ClientRejected += delegate { _rejectAudio.Play(); };
        _client.ClientEat += delegate { _eatAudio.Play(); };

        _client.ClientSetup -= SetupAudioSources;
    }
}
