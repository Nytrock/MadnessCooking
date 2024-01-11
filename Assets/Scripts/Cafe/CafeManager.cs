using System;
using System.Collections.Generic;
using UnityEngine;

public class CafeManager : MonoBehaviour
{
    [SerializeField] private int _spaceCount = 2;
    [SerializeField] private CafeSpace _spacePrefab;
    [SerializeField] private Transform _spaceContainer;

    [SerializeField] private List<CafeSpot> _spots;
    [SerializeField] private List<List<int>> _freeSpots = new List<List<int>>(4);

    public int SpaceCount => _spaceCount;
    public event Action SpaceAdded;

    private void Start()
    {
        SetSpace();
        GenerateSpots();
    }

    private void GenerateSpots()
    {
        // √енерируем споты на основе списка из сохранений
        for (int i = 0; i < 4; i++)
            _freeSpots.Add(new List<int>());
        for (int i = 0; i < _spots.Count; i++)
            _freeSpots[_spots[i].SeatsCount - 1].Add(i);
    }

    private void SetSpace()
    {
        var size = GetSpaceSize();

        for (int i = 0; i < _spaceCount; i++) {
            var space = Instantiate(_spacePrefab);
            space.transform.parent = _spaceContainer;
            space.transform.position += new Vector3(size * i, 0, 0);
        }
    }

    public float GetSpaceSize()
    {
        return _spacePrefab.transform.localScale.x;
    }

    private void AddSpace()
    {
        var space = Instantiate(_spacePrefab);
        space.transform.parent = _spaceContainer;
        space.transform.position += new Vector3(GetSpaceSize() * _spaceCount, 0, 0);
        SpaceAdded?.Invoke();
    }

    public CafeSpot GetRandomSpot(ClientType clientType)
    {
        int needSeat;
        switch (clientType)
        {
            case ClientType.Double:
                needSeat = 2;
                break;
            case ClientType.Triple:
                needSeat = 3;
                break;
            case ClientType.Quarter: 
                needSeat = 4;
                break;
            default:
                needSeat = 1;
                break;
        }
        needSeat -= 1;

        if (_freeSpots[needSeat].Count == 0)
            return null;

        var randomSpotNum = _freeSpots[needSeat][UnityEngine.Random.Range(0, _freeSpots[needSeat].Count)];
        _freeSpots[needSeat].Remove(randomSpotNum);
        return _spots[randomSpotNum];
    }

    public bool CheckHavingSpots()
    {
        int res = 0;
        for (int i = 0; i < _freeSpots.Count; i++)
            res += _freeSpots[i].Count;
        return res != 0;
    }
}
