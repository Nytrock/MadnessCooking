using UnityEngine;

[RequireComponent(typeof(PopularityManager))]
public class PopularityCalculator : MonoBehaviour {
    [SerializeField] private ClientTimeMultiplier _timeMultiplier;
    [SerializeField] private CafeSpaceManager _spaceManager;
    [SerializeField] private FoodManager _foodManager;

    [Header("Multipliers")]
    [SerializeField, Min(0)] private float _oneCafeSpaceMultiplier;
    [SerializeField, Min(0)] private float _oneFoodMultiplier;
    [SerializeField, Min(0)] private float _waitAddForOneSpaceMultiplier = 0.375f;

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
            (1 + (_spaceManager.NoDefaultSpaceCount * _oneCafeSpaceMultiplier)) *
            (1 + (_foodManager.NoDefaultFoodCount * _oneFoodMultiplier));
    }

    public void GetClientChances(out int singleChance, out int doubleChance, out int tripleChance, out int quarterChance) {
        singleChance = _nowLevel.SingleChance;
        doubleChance = _nowLevel.DoubleChance + singleChance;
        tripleChance = _nowLevel.TripleChance + doubleChance;
        quarterChance = _nowLevel.QuarterChance + tripleChance;
    }

    public float GetSpaceMultiplier() {
        return Mathf.Max(1, _spaceManager.SpaceCount * _waitAddForOneSpaceMultiplier);
    }
}
