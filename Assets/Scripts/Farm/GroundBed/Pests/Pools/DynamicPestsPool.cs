using UnityEngine;

public class DynamicPestsPool : PestsPool
{
    [SerializeField] private Pest[] _prefabs;

    [Header("Borders")]
    [SerializeField] private Transform _leftDown;
    [SerializeField] private Transform _rightUp;

    protected override Pest SpawnPest()
    {
        var pest = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], _container);
        pest.SetupBorders(_leftDown, _rightUp);
        return pest;
    }
}
