using UnityEngine;

namespace MadnessCooking.General {
    public abstract class MoneyBaseUI : MonoBehaviour {
        [SerializeField] private MoneyManager _moneyManager;

        protected int _oldCount;
        protected int _nowCount;
        protected bool _isCountAdded;

        protected virtual void Awake() {
            _moneyManager.MoneyChanged += UpdateCount;
            _oldCount = -1;
            _nowCount = -1;
        }

        private void UpdateCount(int newCount) {
            _isCountAdded = newCount > _nowCount;

            _oldCount = _nowCount;
            if (_oldCount == -1)
                _oldCount = newCount;
            _nowCount = newCount;

            StartAnimation();
        }

        protected abstract void StartAnimation();
    }
}
