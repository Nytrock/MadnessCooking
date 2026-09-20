using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class ClientTimeMultiplier : MonoBehaviour {
        [SerializeField] private GameTimeManager _timeManager;
        [SerializeField] private DaytimeMultiplier[] _multipliers;

        private float _nowDaytimeMultiplier = 1;

        public float NowDaytimeMultiplier => _nowDaytimeMultiplier;

        private void Awake() {
            _timeManager.DaytimeChanged += UpdateMultiplier;
        }

        private void UpdateMultiplier(Daytime daytime) {
            foreach (var daytimeMultiplier in _multipliers) {
                if (daytimeMultiplier.Daytime == daytime) {
                    _nowDaytimeMultiplier = daytimeMultiplier.Multiplier;
                    return;
                }
            }
        }
    }
}
