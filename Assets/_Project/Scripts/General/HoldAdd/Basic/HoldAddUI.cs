using UnityEngine;
using UnityEngine.UI;

public class HoldAddUI : MonoBehaviour {
    [SerializeField] protected HoldAdd _holdAdd;
    [SerializeField] private GameObject _UI;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private CountRenderer _readyCount;

    protected virtual void Awake() {
        _holdAdd.ReadyCountChanged += UpdateReadyCount;
        _holdAdd.ClickChanged += ChangeUI;
        _holdAdd.SetupEnded += SetMaxValue;
    }

    private void SetMaxValue() {
        _progressBar.maxValue = _holdAdd.Data.WaitTime;
    }

    private void Update() {
        _progressBar.value = _holdAdd.Data.NowTime;
    }

    public void ChangeUI(bool isWork) {
        _UI.SetActive(isWork);
    }

    public void UpdateReadyCount() {
        _readyCount.UpdateCount(_holdAdd.Data.ReadyCount);
    }
}
