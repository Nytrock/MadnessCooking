using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ingridient")]

public class Ingridient : ScriptableObject, IIngridient
{
    public string Name => _name;
    public string Description => _descr;
    public Sprite ImageIngridient => _img;
    public Sprite ImagePlant => _plant;
    public int TimePlant => _timePlant;
    public int Cost => _cost;

    [SerializeField] private string _name;
    [SerializeField] private string _descr;
    [SerializeField] private Sprite _img;
    [SerializeField] private Sprite _plant;
    [SerializeField] private int _timePlant;
    [SerializeField] private int _cost;
}
