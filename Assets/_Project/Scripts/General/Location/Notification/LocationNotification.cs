using UnityEngine;

namespace MadnessCooking.General {
    public class LocationNotification : MonoBehaviour {
        [SerializeField] private LocationNotificationManager _manager;
        [SerializeField] private GameObject _visual;
        [SerializeField] private Location _location;

        private bool _isShowing;
        private float _nowTime;
        private float _lifeTime;

        private void Awake() {
            _manager.LocationChanged += CheckNewLocation;
            _manager.NotificationCreated += CheckNewNotification;
            _manager.NotificationDestroyed += CheckDestroyedNotification;
            UpdateShowingState(false);
        }

        private void Update() {
            if (!_isShowing || _lifeTime == -1)
                return;

            if (_nowTime < _lifeTime)
                _nowTime += Time.deltaTime;
            else
                UpdateShowingState(false);
        }

        private void CheckNewLocation(Location location) {
            if (_location == location && _isShowing)
                UpdateShowingState(false);
        }

        private void CheckNewNotification(Location location, float lifeTime) {
            if (location != _location)
                return;

            UpdateShowingState(true);
            _lifeTime = lifeTime;
            _nowTime = 0;
        }

        private void CheckDestroyedNotification(Location location) {
            if (location != _location)
                return;

            UpdateShowingState(false);
        }

        private void UpdateShowingState(bool isShowing) {
            _isShowing = isShowing;
            _visual.SetActive(_isShowing);
        }
    }
}
