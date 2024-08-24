using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class CafeStateChangerData {
    [SerializeField, JsonProperty] private bool _isOpened = true;

    public bool IsOpened => _isOpened;

    public void ChangeCafeState() {
        _isOpened = !_isOpened;
    }
}
