using System;
using UnityEngine;

[Serializable]
public class PopularityLevel {
    [SerializeField, Min(1)] private int _needXp;
    [SerializeField, Min(1)] private float _popularityMultiplier = 1;
    [SerializeField] private string _name;
    [TextArea, SerializeField] private string _description;

    [Header("ClientsChances")]
    [SerializeField, Range(0, 1000)] private int _singleChance;
    [SerializeField, Range(0, 1000)] private int _doubleChance;
    [SerializeField, Range(0, 1000)] private int _tripleChance;
    [SerializeField, Range(0, 1000)] private int _quarterChance;

    public int NeedXp => _needXp;
    public float PopularityMultiplier => _popularityMultiplier;
    public string Name => _name;
    public string Description => _description;
    public int SingleChance => _singleChance;
    public int DoubleChance => _doubleChance;
    public int TripleChance => _tripleChance;
    public int QuarterChance => _quarterChance;
}
