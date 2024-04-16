using UnityEngine;

public class CafeSpaceManager : SpaceManager, IBindable<CafeData>
{
    private CafeData _data;

    protected override void AddSpace(float size, int index)
    {
        var space = Instantiate(_spacePrefab, _spaceContainer);
        space.transform.position += new Vector3(size * index, 0, 0);
    }

    public void Bind(CafeData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty) {
            _data.SpaceCount = _defaultSpaceCount;
            LateStart();
            return;
        }

        _spaceCount = _data.SpaceCount;
        LateStart();
    }

    protected override void UpdateData()
    {
        _data.SpaceCount = _spaceCount;
    }
}
