using System;
using UnityEngine;

[Serializable]
public class PopularityLevel
{
    [SerializeField] private int _needXp;
    [SerializeField] private float _popularityMultiplier = 1;
    [SerializeField] private string _name;
    [TextArea, SerializeField] private string _description;

    [Header("ClientsChances")]
    [SerializeField] private float _singleChance;
    [SerializeField] private float _doubleChance;
    [SerializeField] private float _tripleChance;
    [SerializeField] private float _quarterChance;

    public int NeedXp => _needXp;
    public float PopularityMultiplier => _popularityMultiplier;
    public string Name => _name;
    public string Description => _description;
    public float SingleChance => _singleChance;
    public float DoubleChance => _doubleChance;
    public float TripleChance => _tripleChance;
    public float QuarterChance => _quarterChance;
}
