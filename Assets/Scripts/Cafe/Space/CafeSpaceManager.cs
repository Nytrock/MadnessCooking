using UnityEngine;

public class CafeSpaceManager : SpaceManager<CafeData>
{
    protected override void AddSpace(float size, int index)
    {
        var space = Instantiate(_spacePrefab, _spaceContainer);
        space.transform.position += new Vector3(size * index, 0, 0);
    }

    protected override void UpdateData(bool isFileEmpty)
    {
        if (isFileEmpty)
            _data.SpaceCount = _defaultSpaceCount;
        _spaceCount = _data.SpaceCount;
    }
}
