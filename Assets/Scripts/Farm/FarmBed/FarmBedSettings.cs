using System;
using UnityEngine;

[Serializable]
public struct FarmBedSettings
{
    [SerializeField] private FarmBedUIManager _UIManager;
    [SerializeField] private BedChoiceUI _bedChoiceUI;
    [SerializeField] private WheatManager _wheatManager;
    [SerializeField] private FarmCar _car;

    public FarmBedUIManager UIManager => _UIManager;
    public BedChoiceUI BedChoiceUI => _bedChoiceUI;
    public WheatManager WheatManager => _wheatManager;
    public FarmCar Car => _car;
}
