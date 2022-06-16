using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food")]

public class Food : ScriptableObject, FFood
{
    public string Name => _name;
    public string Description => _descr;
    public Technic TypeTechnic => _technic;
    public Sprite ImageFood => _img;
    public float TimeToCooking => _time;
    public int Cost => _cost;
    public int CostBuy => _costBuy;
    public List<Ingridient> NeedIngridients => _recipe;
    public List<int> NumberIngridients => _NumRecipe;

    [SerializeField] private string _name;
    [SerializeField] private string _descr;
    [SerializeField] private Technic _technic;
    [SerializeField] private Sprite _img;
    [SerializeField] private float _time;
    [SerializeField] private int _cost;
    [SerializeField] private int _costBuy;
    [SerializeField] private List<Ingridient> _recipe;
    [SerializeField] private List<int> _NumRecipe;
}
