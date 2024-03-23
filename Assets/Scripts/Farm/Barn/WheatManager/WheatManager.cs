using System;
using UnityEngine;

public class WheatManager : MonoBehaviour, IUpgradeable
{
    private int _wheatCount = 0;

    public event Action<int> WheatChanged;

    public void AddWheat(int count)
    {
        _wheatCount += count;
        WheatChanged?.Invoke(count);
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {

    }

    public void SubstractWheat()
    {
        if (_wheatCount > 0)
            _wheatCount--;
    }
}
