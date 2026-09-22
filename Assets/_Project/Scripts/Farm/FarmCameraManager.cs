using MadnessCooking.General;
using UnityEngine;

namespace MadnessCooking.Farm {
    public class FarmCameraManager : VerticalCameraManager, ISaveable {
        public void LoadSave(GameData data) {
            Vector3 location = _locationManager.GetLocationData(_location).Point;
            data.Farm.CameraManager ??= new(location);
            _data = data.Farm.CameraManager;
        }

        public void LateStart() {
            InvokeCameraMoved();
        }
    }
}
