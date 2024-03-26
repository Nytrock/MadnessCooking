using System;
using UnityEngine;

public class WheatManager : MonoBehaviour, IUpgradeable
{
    [SerializeField] private Cow _cow;
    [SerializeField] private FlourMill _flourMill;
    [SerializeField] private BaseUpgrade _wheatDistributeUpgrade;
    private bool _isDistributing;

    private int _cowWheatCount = 0;
    private int _flourWheatCount = 0;
    private bool _isCowNextWheat = true;

    public event Action<int> CowWheatChanged;
    public event Action<int> FlourWheatChanged;

    public void AddWheat(int count)
    {
        if (_isDistributing) {
            DistributeWheat(count);
        } else {
            _cowWheatCount += count;
            _flourWheatCount += count;
        }

        InvokeEvents();
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _wheatDistributeUpgrade) {
            _isDistributing = true;
            var count = _cowWheatCount;
            _cowWheatCount = 0;
            _flourWheatCount = 0;
            DistributeWheat(count);
            InvokeEvents();
        }
    }

    public void SubstractWheat(Type type)
    {
        if (_isDistributing) {
            if (type == typeof(Cow))
                _cowWheatCount -= 1;
            else if (type == typeof(FlourMill))
                _flourWheatCount -= 1;
            else
                Debug.LogError("Unknown type");
        } else {
            _cowWheatCount -= 1;
            _flourWheatCount -= 1;
        }

        InvokeEvents();
    }

    private void InvokeEvents()
    {
        CowWheatChanged?.Invoke(_cowWheatCount);
        FlourWheatChanged?.Invoke(_flourWheatCount);
    }

    private void DistributeWheat(int count)
    {
        var halfCount = count / 2;
        if (count % 2 == 0) {
            _cowWheatCount += halfCount;
            _flourWheatCount += halfCount;
        } else {
            _cowWheatCount += halfCount + Convert.ToInt32(_isCowNextWheat);
            _flourWheatCount += halfCount + Convert.ToInt32(!_isCowNextWheat);
            _isCowNextWheat = !_isCowNextWheat;
        }
    }
}
