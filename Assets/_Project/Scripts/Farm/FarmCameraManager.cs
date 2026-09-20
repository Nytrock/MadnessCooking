using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Farm {
    public class FarmCameraManager : VerticalCameraManager, IBindable<FarmData> {
        public void Bind(FarmData data) {
            Vector3 location = _locationManager.GetLocationData(_location).Point;
            data.CameraManager ??= new(location);
            _data = data.CameraManager;
        }

        public void LateStart() {
            InvokeCameraMoved();
        }
    }
}
