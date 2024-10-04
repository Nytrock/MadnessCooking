using UnityEngine;
using UnityEngine.UI;

public class TechnicHolderUI : MonoBehaviour {
    [SerializeField] private TechnicHolder _technicHolder;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _repairIcon;
    [SerializeField] private CircleSlider _progressBar;
    private TechnicHolderData _data;

    private void Awake() {
        _technicHolder.CookChanged += UpdateWorkState;
        _technicHolder.RepairChanged += UpdateWorkState;
    }

    private void UpdateWorkState() {
        if (_technicHolder.Data.IsCooking || _technicHolder.Data.IsRepairing)
            StartWork();
        else
            StopWork();
    }

    private void Update() {
        if (_data == null)
            return;

        if (!_data.IsCooking && !_data.IsRepairing)
            return;

        _progressBar.SetValue(_data.NowWaitTime);
    }

    public void SetData(TechnicHolderData data) {
        _data = data;
    }

    public void StartWork() {
        _progressBar.SetMaxValue(_data.NeedWaitTime);
        if (_data.IsCooking)
            _icon.sprite = _data.NowOrder.Food.Icon;
        else if (_data.IsRepairing)
            _icon.sprite = _repairIcon;

        ChangeState(true);
    }

    public void StopWork() {
        ChangeState(false);
    }

    private void ChangeState(bool newState) {
        _panel.SetActive(newState);
    }
}
