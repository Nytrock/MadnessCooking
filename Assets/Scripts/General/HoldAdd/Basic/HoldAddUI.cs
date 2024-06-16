using UnityEngine;
using UnityEngine.UI;

public class HoldAddUI : MonoBehaviour {
    [SerializeField] private GameObject _UI;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private CountRenderer _readyCount;

    public void ChangeUI(bool isWork) {
        _UI.SetActive(isWork);
    }

    public void SetTimeWait(float timeWait) {
        _progressBar.maxValue = timeWait;
    }

    public void UpdateTime(float nowTime) {
        _progressBar.value = nowTime;
    }

    public virtual void UpdateCount(HoldAddData data) {
        _readyCount.UpdateCount(data.ReadyCount);
    }
}
