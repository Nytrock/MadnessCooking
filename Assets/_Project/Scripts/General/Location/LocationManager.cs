using System;
using System.Linq;
using UnityEngine;

namespace MadnessCooking.General {
    public class LocationManager : MonoBehaviour, ISaveable {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LocationPoint[] _locations;
        [SerializeField] private Location[] _saveableLocations;
        [SerializeField] private GameObject _generalUI;

        private LocationManagerData _data;
        private bool _isLateStart = true;

        public event Action<Location> LocationChanged;

        public void LateStart() {
            ChangeLocation(_data.Location);
        }

        public LocationPoint GetLocationData(Location location) {
            foreach (var point in _locations)
                if (point.Location == location)
                    return point;
            throw new ArgumentNullException($"There is no point for {location} location");
        }

        public void ChangeLocation(Location location) {
            LocationPoint locationPoint = GetLocationData(location);

            if (_saveableLocations.Contains(location))
                _data.ChangeLocation(location);
            _mainCamera.transform.position = locationPoint.Point;
            _generalUI.SetActive(!locationPoint.IsHideUI);

            if (_isLateStart)
                _isLateStart = false;
            else
                FatigueManager.Instance.AddFatigue(locationPoint.FatigueCoef);

            LocationChanged?.Invoke(location);
        }

        public void LoadSave(GameData data) {
            data.General.LocationManager ??= new();
            _data = data.General.LocationManager;
        }
    }
}
