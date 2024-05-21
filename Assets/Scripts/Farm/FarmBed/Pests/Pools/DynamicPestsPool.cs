using UnityEngine;

public class DynamicPestsPool : PestsPool
{
    [SerializeField] private Pest[] _prefabs;

    protected override Pest SpawnPest(ref int id)
    {
        if (id == -1)
            id = Random.Range(0, _prefabs.Length);
        var pest = Instantiate(_prefabs[id], _container);
        return pest;
    }
}
