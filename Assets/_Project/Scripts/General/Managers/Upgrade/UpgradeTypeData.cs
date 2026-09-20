using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable]
    public class UpgradeTypeData {
        [SerializeField] private UpgradeType _type;
        [SerializeField] private Sprite _sprite;

        public UpgradeType Type => _type;
        public Sprite Sprite => _sprite;
        public string Name => "UpgradeType." + _type.ToString();
    }
}
