using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TTechnic
{
    string Name { get; }
    string Description { get; }
    Sprite Icon { get; }
    Sprite ActiveIcon { get; }
    Sprite MiniIcon { get; }
    int TimeCooking { get; }
    int Strength { get; }
    int TimeRepairing { get; }
    int CostRepairing { get; }
    int Cost { get; }
}
