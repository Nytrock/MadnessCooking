using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class ChickensAudio : MonoBehaviour {
        [SerializeField] private BarnChickens _chickens;
        [SerializeField] private AudioSource _chickensAudio;
        [SerializeField] private AudioSource _eggAudio;

        private void Awake() {
            _chickens.EggCountChanged += _eggAudio.Play;
            _chickens.FeedStateChanged += ChangeChickensAudioState;
        }

        private void ChangeChickensAudioState() {
            _chickensAudio.ForceChangeState(_chickens.Data.IsFeed);
        }
    }
}
