using System;
using UnityEngine;

[Serializable]
public struct GroundBedSettings
{
    [SerializeField] private GroundBedUIManager _UIManager;
    [SerializeField] private BedChoiceUI _bedChoiceUI;
    [SerializeField] private WheatManager _wheatManager;
    [SerializeField] private Car _car;

    public GroundBedUIManager UIManager => _UIManager;
    public BedChoiceUI BedChoiceUI => _bedChoiceUI;
    public WheatManager WheatManager => _wheatManager;
    public Car Car => _car;
}
