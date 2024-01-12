using System;
using System.Collections.Generic;
using UnityEngine;

public class CafeSpotManager : MonoBehaviour
{
    [SerializeField] private List<CafeSpot> _spots;
    [SerializeField] private List<List<int>> _freeSpots = new List<List<int>>(4);

    private void Start()
    {
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

    public void ReturnSpot(CafeSpot spot)
    {
        _freeSpots[spot.SeatsCount - 1].Add(_spots.IndexOf(spot));
    }
}
