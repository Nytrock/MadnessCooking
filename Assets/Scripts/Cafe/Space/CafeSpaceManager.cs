using UnityEngine;

public class CafeSpaceManager : SpaceManager<CafeData>
{
    protected override void AddSpace(int index)
    {
        var space = Instantiate(_spacePrefab, _spaceContainer);
        space.transform.position += new Vector3(SpaceData.SpaceSize * index, 0, 0);
        InvokeSpaceAdded();
    }

    protected override void BindData(bool isFileEmpty)
    {
        SpaceData = _data.Space;
        if (isFileEmpty)
            SpaceData.Count = _defaultSpaceCount;
        SpaceData.SpaceSize = _spacePrefab.Size;
    }
}
