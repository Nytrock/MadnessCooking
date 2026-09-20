using MadnessCooking.General;
using UnityEngine;

namespace MadnessCooking.Cafe {
    public class CafeCameraManager : HorizontalCameraManager, IBindable<CafeData> {
        public void Bind(CafeData data) {
            Vector3 location = _locationManager.GetLocationData(_location).Point;
            data.CameraManager ??= new(location);
            _data = data.CameraManager;
        }

        public void LateStart() {
            InvokeCameraMoved();
        }
    }
}
