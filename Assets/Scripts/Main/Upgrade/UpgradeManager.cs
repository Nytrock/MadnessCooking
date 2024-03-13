using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [RequireInterface(typeof(IUpgradeable)), SerializeField]
    private MonoBehaviour[] _upgradeablesObjects;
    private IUpgradeable[] _upgradeables;

    private void Start()
    {
        _upgradeables = new IUpgradeable[_upgradeablesObjects.Length];
        for (int i = 0; i < _upgradeablesObjects.Length; i++)
            _upgradeables[i] = _upgradeablesObjects[i].GetComponent<IUpgradeable>();
    }

    public void NewUpgrade(BaseUpgrade upgrade)
    {
        foreach(var upgradeable in _upgradeables) {
            upgradeable.CheckUpgrade(upgrade);
        }
    }
}
