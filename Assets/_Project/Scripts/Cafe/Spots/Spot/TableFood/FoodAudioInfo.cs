using System;
using UnityEngine;

[Serializable]
public class FoodAudioInfo : AudioInfo {
    [SerializeField] private FoodType _foodType;

    public FoodType FoodType => _foodType;
}
