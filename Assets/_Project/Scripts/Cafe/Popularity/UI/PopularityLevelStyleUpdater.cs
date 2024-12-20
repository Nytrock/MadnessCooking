using UnityEngine;

public class PopularityLevelStyleUpdater : MonoBehaviour {
    [SerializeField] private PopularityManager _popularityManager;
    [SerializeField] private IndexStyleUpdater _perStageUpdater;
    [SerializeField] private IndexStyleUpdater _perLevelUpdater;

    private void Awake() {
        _popularityManager.LevelChanged += UpdateInfo;
    }

    public void UpdateInfo(PopularityLevel level) {
        int index = level.Number - 1;
        _perStageUpdater.UpdateStyle(index / 5);
        _perLevelUpdater.UpdateStyle(index % 5);
    }
}

