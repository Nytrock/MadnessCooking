using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public struct JsonColor {
    [SerializeField, JsonProperty] private float _r;
    [SerializeField, JsonProperty] private float _g;
    [SerializeField, JsonProperty] private float _b;
    [SerializeField, JsonProperty] private float _a;

    public JsonColor(Color color) {
        _r = color.r;
        _g = color.g;
        _b = color.b;
        _a = color.a;
    }

    public Color GetColor() {
        return new(_r, _g, _b, _a);
    }
}
