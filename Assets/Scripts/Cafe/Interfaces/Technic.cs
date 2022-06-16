using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Technic")]

public class Technic : ScriptableObject, TTechnic { 
    public string Name => _name;
    public string Description => _descr;
    public Sprite Icon => _icon;
    public Sprite MiniIcon => _MiniIcon;
    public Sprite ActiveIcon => _ActiveIcon;
    public int TimeCooking => _time;
    public int TimeRepairing => _repair;
    public int Strength => _strength;
    public int CostRepairing => _costRepair;
    public int Cost => _cost;

    [SerializeField] private string _name;
    [SerializeField] private string _descr;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _MiniIcon;
    [SerializeField] private Sprite _ActiveIcon;
    [SerializeField] private int _time;
    [SerializeField] private int _repair;
    [SerializeField] private int _strength;
    [SerializeField] private int _costRepair;
    [SerializeField] private int _cost;
}
