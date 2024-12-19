using System;
using UnityEngine;

public class CursorManager : MonoBehaviour, IBindable<GameSettingsData>, ISettingableWithOptions {
    [SerializeField] private CursorTexture[] _cursors;
    [SerializeField] private CursorTexture _defaultCursor;
    [SerializeField] private Vector2 _offset; private SettingsPointData<int> _data;
    private CursorTexture _nowCursor;

    public int OptionsCount => _cursors.Length;
    public int DefaultValue => Mathf.Max(Array.IndexOf(_cursors, _defaultCursor), 0);

    public void Bind(GameSettingsData data) {
        data.CursorManager ??= new(DefaultValue);
        _data = data.CursorManager;
    }

    public void LateStart() {
        UpdateValue();
    }

    public void UpdateValue() {
        _nowCursor = _cursors[_data.LastValue];
        SetCursorTexture(CursorState.Standard);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            SetCursorTexture(CursorState.Press);
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            SetCursorTexture(CursorState.Standard);
    }

    public Sprite GetNowCursor() {
        return _nowCursor.Icon;
    }

    private void SetCursorTexture(CursorState state) {
        Texture2D texture = _nowCursor.GetCursor(state);
        Cursor.SetCursor(texture, _offset, CursorMode.ForceSoftware);
    }
}
