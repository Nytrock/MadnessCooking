using UnityEngine;

public class TimeRenderChange : MonoBehaviour, IUpgradeable, IBindable<MainData>
{
    [SerializeField] private BaseUpgrade _clockUpgrade;
    [SerializeField] private TimeRenderClock _clock;
    [SerializeField] private TimeRenderWatch _watch;
    private MainData _data;

    private void LateStart()
    {
        ChangeTimeRenderer();
    }

    public void CheckUpgrade(BaseUpgrade upgrade)
    {
        _data.IsUpgradedTimeRenderer |= upgrade == _clockUpgrade;
        ChangeTimeRenderer();
    }

    private void ChangeTimeRenderer()
    {
        _clock.gameObject.SetActive(!_data.IsUpgradedTimeRenderer);
        _watch.gameObject.SetActive(_data.IsUpgradedTimeRenderer);
    }

    public void Bind(MainData data, bool isFileEmpty)
    {
        _data = data;
        LateStart();
    }
}
