using UnityEngine;

public class CafeSpaceManager : SpaceManager<CafeData> {
    protected override void AddSpace(int index) {
        SpacePrefab space = Instantiate(_spacePrefab, _spaceContainer);
        space.transform.position += new Vector3(_spacePrefab.Size * index, 0, 0);
        InvokeSpaceAdded();
    }

    protected override void BindData(bool isFileEmpty) {
        if (isFileEmpty)
            _data.Space = new(_defaultSpaceCount);
        _spaceData = _data.Space;
    }
}
