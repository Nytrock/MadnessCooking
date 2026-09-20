using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Office {
    public class InternetDownloadAudio : MonoBehaviour {
        [SerializeField] private InternetDownload _download;
        [SerializeField] private AudioSource _downloadAudio;
        [SerializeField] private AudioSource _endAudio;

        private void Awake() {
            _download.LoadingUpdated += _downloadAudio.Play;
            _download.LoadingEnded += _endAudio.Play;
        }
    }
}
