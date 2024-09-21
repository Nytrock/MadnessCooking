using UnityEngine;

public class PopularityLevelStyleUpdater : MonoBehaviour {
    [SerializeField] private PopularityManager _popularityManager;
    [SerializeField] private IndexStyleUpdater _perStageUpdater;
    [SerializeField] private IndexStyleUpdater _perLevelUpdater;

    private void Awake() {
        _popularityManager.LevelChanged += UpdateInfo;
    }

    public void UpdateInfo(PopularityLevel level) {
        _perStageUpdater.UpdateStyle(level.Number / 5);
        _perLevelUpdater.UpdateStyle(level.Number % 5);
    }
}

