using UnityEngine;
using UnityEngine.UI;

public class HoldAddUI : MonoBehaviour {
    [SerializeField] protected HoldAdd _holdAdd;
    [SerializeField] private GameObject _UI;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private CountRenderer _readyCount;

    private void Awake() {
        _holdAdd.CountChanged += UpdateCount;
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

    public virtual void UpdateCount() {
        _readyCount.UpdateCount(_holdAdd.Data.ReadyCount);
    }
}
