using UnityEngine;

[RequireComponent(typeof(PopularityManager))]
public class PopularityCalculator : MonoBehaviour
{
    [SerializeField] private ClientTimeMultiplier _timeMultiplier;
    [SerializeField] private CafeSpaceManager _spaceManager;
    [SerializeField] private FoodManager _foodManager;

    [Header("Multipliers")]
    [SerializeField] private float _oneCafeSpaceMultiplier;
    [SerializeField] private float _oneFoodMultiplier;

    private PopularityLevel _nowLevel;
    private PopularityManager _popularityManager;

    private void Awake()
    {
        _popularityManager = GetComponent<PopularityManager>();
        _popularityManager.LevelChanged += UpdateLevel;
    }

    private void UpdateLevel(PopularityLevel level)
    {
        _nowLevel = level;
    }

    public float GetPopularity()
    {
        return _nowLevel.PopularityMultiplier * 
            _timeMultiplier.DaytimeMultiplier * 
            (1 + _spaceManager.SpaceCount * _oneCafeSpaceMultiplier) * 
            (1 + _foodManager.FoodCount * _oneFoodMultiplier);
    }

    public void GetClientsNumberChances(out int singleChance, out int doubleChance, out int tripleChance, out int quarterChance)
    {
        singleChance = (int)(_nowLevel.SingleChance * 10);
        doubleChance = (int)(_nowLevel.DoubleChance * 10);
        tripleChance = (int)(_nowLevel.TripleChance * 10);
        quarterChance = (int)(_nowLevel.QuarterChance * 10);

        doubleChance += singleChance;
        tripleChance += doubleChance;
        quarterChance += tripleChance;
    }

    public float GetSpaceMultiplier()
    {
        return Mathf.Max(1, _spaceManager.SpaceCount * 0.375f);
    }
}
