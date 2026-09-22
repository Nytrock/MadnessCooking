using System;
using UnityEngine;

namespace MadnessCooking.General {
    public class CursorManager : MonoBehaviour, ISettingableWithOptions {
        [SerializeField] private CursorTexture[] _cursors;
        [SerializeField] private CursorTexture _defaultCursor;

        private SettingsPointData<int> _data;
        private CursorTexture _nowCursor;

        public int OptionsCount => _cursors.Length;
        public int DefaultValue => Mathf.Max(Array.IndexOf(_cursors, _defaultCursor), 0);

        public void SetSettings(SettingsData data) {
            data.GameSettings.CursorManager ??= new(DefaultValue);
            _data = data.GameSettings.CursorManager;
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
            if (Application.isMobilePlatform)
                return;

            Texture2D texture = _nowCursor.GetCursor(state);
            Cursor.SetCursor(texture, _nowCursor.Offset, CursorMode.ForceSoftware);
        }
    }
}
