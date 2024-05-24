using System;
using UnityEngine;

[Serializable]
public class PopularityLevel
{
    [SerializeField, Min(1)] private int _needXp;
    [SerializeField, Min(1)] private float _popularityMultiplier = 1;
    [SerializeField] private string _name;
    [TextArea, SerializeField] private string _description;

    [Header("ClientsChances")]
    [SerializeField, Range(0, 100)] private float _singleChance;
    [SerializeField, Range(0, 100)] private float _doubleChance;
    [SerializeField, Range(0, 100)] private float _tripleChance;
    [SerializeField, Range(0, 100)] private float _quarterChance;

    public int NeedXp => _needXp;
    public float PopularityMultiplier => _popularityMultiplier;
    public string Name => _name;
    public string Description => _description;
    public float SingleChance => _singleChance;
    public float DoubleChance => _doubleChance;
    public float TripleChance => _tripleChance;
    public float QuarterChance => _quarterChance;
}
