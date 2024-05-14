using System;
using System.Linq;
using UnityEngine;

public class FarmCar : IngredientStorage<FarmData>, IUpgradeable
{
    [SerializeField] private Animator _animator;

    [Header("Upgrades")]
    [SerializeField] private CountUpgrade[] _sizeUpgrades;

    public event Action CarReturned;

    public override void Bind(FarmData data, bool isFileEmpty)
    {
        throw new NotImplementedException();
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (_sizeUpgrades.Contains(upgrade)) {
            var countUpgrade = upgrade as CountUpgrade;
            _maxSize = countUpgrade.Count;
            InvokeSizeChange();
        }
    }

    public void Leave()
    {
        _ingredients.Clear();
        _nowSize = 0;
        _animator.SetBool("isLeave", true);
    }

    public void Return()
    {
        _animator.SetBool("isLeave", false);
        CarReturned?.Invoke();
    }

    protected override void UpdateData()
    {

    }
}
