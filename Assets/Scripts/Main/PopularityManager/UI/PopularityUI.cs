using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopularityUI : MonoBehaviour
{
    [SerializeField] private PopularityManager _popularityManager;
    [SerializeField] private PopularityUIMore _additionalUI;

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private Slider _progress;

    private bool _isLastLevel;

    private void Start()
    {
        _popularityManager.LevelChanged += UpdateLevel;
        _popularityManager.XpChanged += UpdateProgress;
    }

    private void UpdateLevel(PopularityLevel level)
    {
        _name.text = level.Name;
        _level.text = (_popularityManager.NowLevel + 1).ToString();
        _additionalUI.UpdateInfo(level);

        if (_popularityManager.IsMaxLevel) {
            _isLastLevel = true;
            _progress.value = _progress.maxValue;
        } else {
            _progress.maxValue = level.NeedXp;
        }
    }

    private void UpdateProgress(int xp)
    {
        if (_isLastLevel)
            return;

        _progress.value = xp;
    }
}
