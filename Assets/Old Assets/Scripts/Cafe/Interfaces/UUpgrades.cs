using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UUpgrades
{
    string Name { get; }
    string Description { get; }
    string Type { get; }
    int Cost { get; }
    Sprite SpriteUpgrade { get; }
}
