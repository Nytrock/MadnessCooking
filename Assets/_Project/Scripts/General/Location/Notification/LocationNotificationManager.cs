using System;
using UnityEngine;

namespace MadnessCooking.General {
    [RequireComponent(typeof(LocationManager))]
    public class LocationNotificationManager : MonoBehaviour {
        private LocationManager _locationManager;
        private Location _nowLocation;

        public event Action<Location> LocationChanged;
        public event Action<Location, float> NotificationCreated;
        public event Action<Location> NotificationDestroyed;

        private void Awake() {
            _locationManager = GetComponent<LocationManager>();
            _locationManager.LocationChanged += OnLocationChanged;
        }

        private void OnLocationChanged(Location location) {
            _nowLocation = location;
            LocationChanged?.Invoke(location);
        }

        public void CreateNotification(Location location, float lifeTime = -1) {
            if (_nowLocation == location)
                return;

            NotificationCreated?.Invoke(location, lifeTime);
        }

        public void DestroyNotification(Location location) {
            NotificationDestroyed?.Invoke(location);
        }
    }
}
