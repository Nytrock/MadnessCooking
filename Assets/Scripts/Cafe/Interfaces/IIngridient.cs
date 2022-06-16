using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIngridient
{
    string Name { get; }
    string Description { get; }
    Sprite ImageIngridient { get; }
    Sprite ImagePlant { get; }
    int TimePlant { get; }
    int Cost { get; }
}
