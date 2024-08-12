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
        UpdateValue();
    }

    public void UpdateValue() {
        _nowCursor = _cursors[_data.LastValue];
        SetCursorTexture(_nowCursor.MainTexture);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
            Texture2D texture = _nowCursor.GetCursor(CursorState.Press);
            SetCursorTexture(texture);
        } else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) {
            SetCursorTexture(_nowCursor.MainTexture);
        }
    }

    public Sprite GetNowCursor() {
        Texture2D texture = _nowCursor.MainTexture;
        return Sprite.Create(texture, new(0, 0, texture.width, texture.height), Vector2.zero);
    }

    private void SetCursorTexture(Texture2D texture) {
        Cursor.SetCursor(texture, _offset, CursorMode.ForceSoftware);
    }
}
