using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MadnessCooking.General {
    [Serializable, JsonObject(MemberSerialization.OptIn)]
    public class CameraManagerData {
        [SerializeField, JsonProperty] private Vector3 _cameraPosition;

        public Vector3 CameraPosition => _cameraPosition;

        public CameraManagerData(Vector3 defaultPosition) {
            _cameraPosition = defaultPosition;
        }

        public void UpdateCameraPosition(Vector3 newPosition) {
            _cameraPosition = newPosition;
        }
    }
}
