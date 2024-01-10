using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade")]

public class Upgrade : ScriptableObject, UUpgrades
{
    public string Name => _name;
    public string Description => _descr;
    public string Type => _type;
    public int Cost => _cost;
    public Sprite SpriteUpgrade => _sp;

    [SerializeField] private string _name;
    [SerializeField] private string _descr;
    [SerializeField] private string _type;
    [SerializeField] private int _cost;
    [SerializeField] private Sprite _sp;
}
