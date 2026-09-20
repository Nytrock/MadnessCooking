using System;
using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    [Serializable]
    public struct FarmBedSettings {
        [SerializeField] private FarmBedUIManager _UIManager;
        [SerializeField] private BedChoiceUI _bedChoiceUI;
        [SerializeField] private WheatManager _wheatManager;
        [SerializeField] private FarmCar _car;
        [SerializeField] private Puncher _puncher;

        public FarmBedUIManager UIManager => _UIManager;
        public BedChoiceUI BedChoiceUI => _bedChoiceUI;
        public WheatManager WheatManager => _wheatManager;
        public FarmCar Car => _car;
        public Puncher Puncher => _puncher;
    }
}
