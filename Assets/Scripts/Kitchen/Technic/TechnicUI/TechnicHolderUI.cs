using UnityEngine;
using UnityEngine.UI;

public class TechnicHolderUI : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _repairIcon;
    [SerializeField] private Button _UIButton;
    [SerializeField] private CircleSlider _progressBar;
    private TechnicHolderData _data;

    private void Awake() {
        ChangeState(false);
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
        else if (_data.IsCooking)
            _icon.sprite = _repairIcon;

        ChangeState(true);
    }

    public void StopWork() {
        ChangeState(false);
    }

    private void ChangeState(bool newState) {
        _panel.SetActive(newState);
        _UIButton.interactable = !newState;
    }

    public void SetRepairUI(TechnicRepairUI repairUI, TechnicHolder holder) {
        _UIButton.onClick.AddListener(delegate { repairUI.OpenTechnic(holder); });
    }
}
