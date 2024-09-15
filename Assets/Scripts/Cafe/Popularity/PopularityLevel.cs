using System;
using UnityEngine;

[CreateAssetMenu(menuName = nameof(PopularityLevel))]
public class PopularityLevel : ScriptableObject {
    [SerializeField, Min(1)] private int _number;
    [SerializeField, Min(1)] private int _needXp;
    [SerializeField, Min(1)] private float _popularityMultiplier = 1;

    [Header("ClientsChances")]
    [SerializeField, Range(0, 1000)] private int _singleChance;
    [SerializeField, Range(0, 1000)] private int _doubleChance;
    [SerializeField, Range(0, 1000)] private int _tripleChance;
    [SerializeField, Range(0, 1000)] private int _quarterChance;

    public int Number => _number;
    public int NeedXp => _needXp;
    public float PopularityMultiplier => _popularityMultiplier;
    public string Description => "Popularity" + name + ".Description";
    public int SingleChance => _singleChance;
    public int DoubleChance => _doubleChance;
    public int TripleChance => _tripleChance;
    public int QuarterChance => _quarterChance;
}
