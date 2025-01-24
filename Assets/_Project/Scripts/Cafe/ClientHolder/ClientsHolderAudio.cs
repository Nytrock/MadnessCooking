using UnityEngine;

public class ClientsHolderAudio : MonoBehaviour {
    [SerializeField] private ClientsHolder _holder;
    [SerializeField] private RandomAudioSource _talkAudio;

    private void Awake() {
        _holder.TalkStarted += delegate { _talkAudio.ForceChangeState(true); };
        _holder.ClientsLeaved += delegate { _talkAudio.ForceChangeState(false); };
    }
}
