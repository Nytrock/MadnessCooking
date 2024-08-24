using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class WheatManagerData {
    [SerializeField, JsonProperty] private bool _isCowNextWheat;

    public bool IsCowNextWheat => _isCowNextWheat;

    public void ChangeCowNextWheat() {
        _isCowNextWheat = !_isCowNextWheat;
    }
}
