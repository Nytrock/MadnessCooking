using System;
using UnityEngine;

namespace MadnessCooking.General {
    public abstract class SpaceManager : MonoBehaviour, ISaveable {
        [SerializeField] protected UpgradeManager _upgradeManager;
        [SerializeField] protected int _defaultSpaceCount;
        [SerializeField] protected CountUpgrade[] _spaceAddUpgrades;
        [SerializeField] protected SpacePrefab _spacePrefab;

        protected SpaceManagerData _spaceData;
        protected Transform _spaceContainer;

        public event Action SpaceAdded;

        public int SpaceCount => _spaceData.Count;
        public int NoDefaultSpaceCount => Mathf.Max(0, SpaceCount - _defaultSpaceCount);
        public float SpaceSize => _spacePrefab.Size;

        protected virtual void Awake() {
            _spaceContainer = transform;
            _upgradeManager.ItemAdded += CheckSpaceAdded;
        }

        protected void InvokeSpaceAdded() {
            SpaceAdded?.Invoke();
        }

        public void LateStart() {
            GenerateSpaces();
        }

        private void GenerateSpaces() {
            for (int i = 0; i < _spaceData.Count; i++)
                AddSpace(i);
        }

        public virtual void CheckSpaceAdded(BaseUpgrade upgrade) {
            if (_spaceData.Count - _defaultSpaceCount >= _spaceAddUpgrades.Length)
                return;

            if (upgrade == _spaceAddUpgrades[_spaceData.Count - _defaultSpaceCount]) {
                _spaceData.SetCount(upgrade as CountUpgrade);
                AddSpace(_spaceData.Count - 1);
            }
        }

        public float GetSpacesSize() {
            return (_spaceData.Count - 1) * SpaceSize;
        }

        protected abstract void AddSpace(int index);
        public abstract void LoadSave(GameData data);
    }
}
