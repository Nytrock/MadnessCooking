using System;
using TMPro;
using UnityEngine;

public class ScreenSizeSettingsPoint : IntSettingsPoint, IBindable<VideoSettingsData> {
    [SerializeField] private TextMeshProUGUI _text;

    protected override void Awake() {
        base.Awake();
        if ((_settingable.Value as ScreenSizeManager) == null)
            throw new NullReferenceException($"{_settingable} is not ScreenSizeManager");
    }

    protected override void UpdateState() {
        base.UpdateState();
        ScreenSize nowScreenSize = (_settingable.Value as ScreenSizeManager).GetNowScreenSize();
        _text.text = nowScreenSize.ToString();
    }

    public void ChangeState(bool newState) {
        gameObject.SetActive(newState);
    }

    public void LateStart() {
        UpdateState();
    }

    public void Bind(VideoSettingsData data) {
        _data = data.ScreenSize;
    }
}
