using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FFood
{
    string Name { get; }
    Technic TypeTechnic { get; }
    Sprite ImageFood { get; }
    float TimeToCooking { get;  }
    int Cost { get; }
    int CostBuy { get; }
    List<Ingridient> NeedIngridients { get; }
    List<int> NumberIngridients { get; }
}
