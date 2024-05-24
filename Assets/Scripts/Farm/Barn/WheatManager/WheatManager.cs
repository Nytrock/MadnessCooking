using System;
using UnityEngine;

public class WheatManager : MonoBehaviour, IUpgradeable, IBindable<FarmData>
{
    [SerializeField] private Cow _cow;
    [SerializeField] private FlourMill _flourMill;
    [SerializeField] private BaseUpgrade _wheatDistributeUpgrade;
    private FarmData _data;

    public void AddWheat(int count)
    {
        if (_data.IsWheatDistributing) {
            DistributeWheat(count);
        } else {
            _data.Cow.MaterialCount += count;
            _data.FlourMill.MaterialCount += count;
        }
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _wheatDistributeUpgrade) {
            _data.IsWheatDistributing = true;
            int count = _data.Cow.MaterialCount;
            _data.Cow.MaterialCount = 0;
            _data.FlourMill.MaterialCount = 0;
            DistributeWheat(count);
        }
    }

    private void DistributeWheat(int count)
    {
        int halfCount = count / 2;

        _data.Cow.MaterialCount += halfCount;
        _data.FlourMill.MaterialCount += halfCount;

        if (count % 2 != 0) {
            _data.Cow.MaterialCount += Convert.ToInt32(_data.IsCowNextWheat);
            _data.FlourMill.MaterialCount += Convert.ToInt32(!_data.IsCowNextWheat);
            _data.IsCowNextWheat = !_data.IsCowNextWheat;
        }
    }

    public void Bind(FarmData data, bool isFileEmpty)
    {
        _data = data;
    }
}
