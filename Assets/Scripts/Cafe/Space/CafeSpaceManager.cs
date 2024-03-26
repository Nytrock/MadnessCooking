using UnityEngine;

public class CafeSpaceManager : SpaceManager
{
    protected override void AddSpace(float size, int index)
    {
        var space = Instantiate(_spacePrefab, _spaceContainer);
        space.transform.position += new Vector3(size * index, 0, 0);
    }
}
