using UnityEngine;
using UnityEngine.UI;

public class HoldAddUI : MonoBehaviour
{
    [SerializeField] private GameObject _UI;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private CountRenderer _readyCount;
    protected HoldAdd _holdAdd;

    protected virtual void Update()
    {
        _progressBar.value = _holdAdd.HoldData.NowTime;
        _readyCount.UpdateCount(_holdAdd.HoldData.ReadyCount);
    }

    public void ChangeUI(bool isWork)
    {
        _UI.SetActive(isWork);
    }

    public void Setup(HoldAdd holdAdd)
    {
        _holdAdd = holdAdd;
        _progressBar.maxValue = _holdAdd.TimeWait;
    }
}
