using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class CafeNameManagerData {
    [SerializeField, JsonProperty] private string _cafeName;

    public string CafeName => _cafeName;

    public CafeNameManagerData() {
        _cafeName = string.Empty;
    }

    public void ChangeCafeName(string cafeName) {
        _cafeName = cafeName;
    }
}
