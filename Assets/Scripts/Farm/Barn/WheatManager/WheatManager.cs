using System;
using UnityEngine;

public class WheatManager : MonoBehaviour
{
    private int _wheatCount = 0;

    public int WheatCount => _wheatCount;

    public event Action<int> WheatChanged;

    public void AddWheat(int count)
    {
        _wheatCount += count;
        WheatChanged?.Invoke(count);
    }

    public void SubstractWheat()
    {
        if (_wheatCount > 0)
            _wheatCount--;
    }
}
