using UnityEngine;

public class TimeRenderChange : MonoBehaviour, IUpgradeable
{
    [SerializeField] private BaseUpgrade _clockUpgrade;
    [SerializeField] private TimeRenderClock _clock;
    [SerializeField] private TimeRenderWatch _watch;
    private bool _isUpgraded;

    private void Start()
    {
        ChangeTimeRenderer(_isUpgraded);
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        _isUpgraded |= upgrade == _clockUpgrade;
        ChangeTimeRenderer(_isUpgraded);
    }

    private void ChangeTimeRenderer(bool isUpgraded)
    {
        _clock.gameObject.SetActive(!isUpgraded);
        _watch.gameObject.SetActive(isUpgraded);
    }
}
