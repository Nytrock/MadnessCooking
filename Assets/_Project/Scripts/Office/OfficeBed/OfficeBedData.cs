using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class OfficeBedData {
    [SerializeField, JsonProperty] private bool _isSleep;

    public bool IsSleep => _isSleep;

    public void ChangeSleepState() {
        _isSleep = !_isSleep;
    }
}
