using System;
using UnityEngine;

public class CursorManager : MonoBehaviour, IBindable<GameSettingsData>, ISettingableWithOptions {
    [SerializeField] private Texture2D[] _cursors;
    [SerializeField] private Texture2D _defaultCursor;
    [SerializeField] private Vector2 _offset;
    private SettingsPointData<int> _data;

    public int OptionsCount => _cursors.Length;
    public int DefaultValue => Mathf.Max(Array.IndexOf(_cursors, _defaultCursor), 0);

    public void Bind(GameSettingsData data) {
        data.CursorManager ??= new(DefaultValue);
        _data = data.CursorManager;
        UpdateValue();
    }

    public void UpdateValue() {
        Cursor.SetCursor(_cursors[_data.LastValue], _offset, CursorMode.ForceSoftware);
    }

    public Sprite GetNowCursor() {
        return Sprite.Create(_cursors[_data.LastValue], new(0, 0, 19, 19), Vector2.zero);
    }
}
