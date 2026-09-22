using MadnessCooking.General;
using UnityEngine;

namespace MadnessCooking.Cafe {
    public class CafeCameraManager : HorizontalCameraManager, ISaveable {
        public void LoadSave(GameData data) {
            Vector3 location = _locationManager.GetLocationData(_location).Point;
            data.Cafe.CameraManager ??= new(location);
            _data = data.Cafe.CameraManager;
        }

        public void LateStart() {
            InvokeCameraMoved();
        }
    }
}
