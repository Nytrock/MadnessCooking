using TMPro;
using UnityEngine;
using UnityEngine.UI;
using MadnessCooking.General;

namespace MadnessCooking.Cafe {
    public class PopularityUI : MonoBehaviour {
        [SerializeField] private PopularityManager _popularityManager;
        [SerializeField] private PopularityUIMore _additionalUI;

        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Slider _progress;

        private void Awake() {
            _popularityManager.LevelChanged += UpdateLevel;
            _popularityManager.XpChanged += UpdateProgress;
        }

        private void UpdateLevel(PopularityLevel level) {
            _levelText.text = level.Number.ToString();
            _additionalUI.UpdateInfo(level);

            if (_popularityManager.IsMaxLevel)
                _progress.value = _progress.maxValue;
            else
                _progress.maxValue = level.NeedXp;
        }

        private void UpdateProgress(int xp) {
            _additionalUI.ChangeCriticWaitText(_popularityManager.CheckLevelWaitCritic());

            if (_popularityManager.IsMaxLevel)
                return;

            _progress.value = xp;
        }
    }
}
