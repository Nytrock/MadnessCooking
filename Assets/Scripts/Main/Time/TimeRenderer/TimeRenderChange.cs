using UnityEngine;

public class TimeRenderChange : MonoBehaviour, IUpgradeable, IBindable<MainData>
{
    [SerializeField] private BaseUpgrade _clockUpgrade;
    [SerializeField] private TimeRenderClock _clock;
    [SerializeField] private TimeRenderWatch _watch;
    private bool _isUpgraded;
    private MainData _data;

    private void LateStart()
    {
        ChangeTimeRenderer();
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        _isUpgraded |= upgrade == _clockUpgrade;
        _data.IsUpgradedTimeRenderer = _isUpgraded;
        ChangeTimeRenderer();
    }

    private void ChangeTimeRenderer()
    {
        _clock.gameObject.SetActive(!_isUpgraded);
        _watch.gameObject.SetActive(_isUpgraded);
    }

    public void Bind(MainData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty) {
            _data.IsUpgradedTimeRenderer = _isUpgraded;
            LateStart();
            return;
        }

        _isUpgraded = _data.IsUpgradedTimeRenderer;
        LateStart();
    }
}
