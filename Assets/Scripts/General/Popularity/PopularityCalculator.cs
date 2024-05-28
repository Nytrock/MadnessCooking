using UnityEngine;

[RequireComponent(typeof(PopularityManager))]
public class PopularityCalculator : MonoBehaviour {
    [SerializeField] private ClientTimeMultiplier _timeMultiplier;
    [SerializeField] private CafeSpaceManager _spaceManager;
    [SerializeField] private FoodManager _foodManager;

    [Header("Multipliers")]
    [SerializeField, Min(0)] private float _oneCafeSpaceMultiplier;
    [SerializeField, Min(0)] private float _oneFoodMultiplier;

    private PopularityLevel _nowLevel;
    private PopularityManager _popularityManager;

    private void Awake() {
        _popularityManager = GetComponent<PopularityManager>();
        _popularityManager.LevelChanged += UpdateLevel;
    }

    private void UpdateLevel(PopularityLevel level) {
        _nowLevel = level;
    }

    public float GetPopularity() {
        return _nowLevel.PopularityMultiplier *
            _timeMultiplier.DaytimeMultiplier *
            (1 + (_spaceManager.SpaceData.Count * _oneCafeSpaceMultiplier)) *
            (1 + (_foodManager.FoodCount * _oneFoodMultiplier));
    }

    public void GetClientChances(out int singleChance, out int doubleChance, out int tripleChance, out int quarterChance) {
        singleChance = (int)(_nowLevel.SingleChance * 10);
        doubleChance = (int)(_nowLevel.DoubleChance * 10);
        tripleChance = (int)(_nowLevel.TripleChance * 10);
        quarterChance = (int)(_nowLevel.QuarterChance * 10);

        doubleChance += singleChance;
        tripleChance += doubleChance;
        quarterChance += tripleChance;
    }

    public float GetSpaceMultiplier() => Mathf.Max(1, _spaceManager.SpaceData.Count * 0.375f);
}
