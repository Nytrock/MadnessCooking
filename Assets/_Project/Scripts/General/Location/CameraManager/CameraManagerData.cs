using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class CameraManagerData {
    [SerializeField, JsonProperty] private JsonVector _cameraPosition;

    public Vector3 CameraPosition => _cameraPosition.GetVector();

    public CameraManagerData(Vector3 defaultPosition) {
        _cameraPosition = new(defaultPosition);
    }

    public void UpdateCameraPosition(Vector3 newPosition) {
        _cameraPosition = new(newPosition);
    }
}
