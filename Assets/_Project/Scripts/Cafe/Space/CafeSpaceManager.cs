using MadnessCooking.General;
using UnityEngine;

namespace MadnessCooking.Cafe {
    public class CafeSpaceManager : SpaceManager {
        protected override void AddSpace(int index) {
            SpacePrefab space = Instantiate(_spacePrefab, _spaceContainer);
            space.transform.position += new Vector3(_spacePrefab.Size * index, 0, 0);
            InvokeSpaceAdded();
        }

        public override void LoadSave(GameData data) {
            data.Cafe.SpaceManager ??= new(_defaultSpaceCount);
            _spaceData = data.Cafe.SpaceManager;
        }
    }
}
