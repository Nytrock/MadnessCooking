using System;
using UnityEngine;
using UnityEngine.UI;

public class CursorSettingsPoint : IntSettingsPoint, IBindable<GameSettingsData> {
    [SerializeField] private Image _cursorImage;

    protected override void Awake() {
        base.Awake();
        if ((_settingable.Value as CursorManager) == null)
            throw new NullReferenceException($"{_settingable} is not cursorManager");
    }

    public void Bind(GameSettingsData data) {
        _data = data.CursorManager;
        UpdateState();
    }

    protected override void UpdateState() {
        base.UpdateState();
        _cursorImage.sprite = (_settingable.Value as CursorManager).GetNowCursor();
    }
}
