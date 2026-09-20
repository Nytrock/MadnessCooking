using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class MoneyManagerData {
        [SerializeField, JsonProperty] private int _moneyCount;

        public int MoneyCount => _moneyCount;

        public MoneyManagerData(int moneyDefault) {
            _moneyCount = moneyDefault;
        }

        public void ChangeMoneyCount(int changeValue) {
            if (_moneyCount + changeValue < 0)
                return;

            _moneyCount += changeValue;
        }
    }
}
