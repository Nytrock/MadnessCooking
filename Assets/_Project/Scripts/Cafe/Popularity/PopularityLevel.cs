using System;
using UnityEngine;

[CreateAssetMenu(menuName = nameof(PopularityLevel))]
public class PopularityLevel : ScriptableObject {
    [SerializeField, Min(1)] private int _number;
    [SerializeField, Min(1)] private int _needXp;
    [SerializeField, Min(1)] private float _popularityMultiplier = 1;

    [Header("ClientsChances")]
    [SerializeField, Range(0, 100)] private float _singleChance;
    [SerializeField, Range(0, 100)] private float _doubleChance;
    [SerializeField, Range(0, 100)] private float _tripleChance;
    [SerializeField, Range(0, 100)] private float _quarterChance;

    private string _description;

    public int Number => _number;
    public int NeedXp => _needXp;
    public float PopularityMultiplier => _popularityMultiplier;
    public string Description => _description;
    public float SingleChance => _singleChance;
    public float DoubleChance => _doubleChance;
    public float TripleChance => _tripleChance;
    public float QuarterChance => _quarterChance;

    public void Initialize() {
        _description = "Popularity" + name + ".Description";
    }
}
