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
            _timeMultiplier.NowDaytimeMultiplier *
            (1 + (_spaceManager.NoDefaultSpaceCount * _oneCafeSpaceMultiplier)) *
            (1 + (_foodManager.FoodCountWithoutDefault * _oneFoodMultiplier));
    }

    public ClientCount GetClientCount() {
        float singleChance = _nowLevel.SingleChance;
        float doubleChance = _nowLevel.DoubleChance + singleChance;
        float tripleChance = _nowLevel.TripleChance + doubleChance;

        float chance = Random.Range(1f, 101f);
        if (chance <= singleChance)
            return ClientCount.One;
        else if (chance <= doubleChance)
            return ClientCount.Two;
        else if (chance <= tripleChance)
            return ClientCount.Three;
        return ClientCount.Four;
    }
}
