using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationManager : Singleton<LocalizationManager>, IBindable<GameSettingsData>, ISettingableWithOptions {
    [SerializeField] private Locale[] _locales;
    [SerializeField] private Locale _defaultLocale;
    private SettingsPointData<int> _data;

    public int OptionsCount => _locales.Length;
    public int DefaultValue => Mathf.Max(Array.IndexOf(_locales, _defaultLocale), 0);

    public event Action LocalizationChanged;

    protected override void Awake() {
        base.Awake();
        LocalizationSettings.SelectedLocaleChanged += delegate { UpdateLocalization(); };
    }

    private void UpdateLocalization() {
        if (!Application.isPlaying) return;

        LocalizationChanged?.Invoke();
    }

    public string GetLocalization(string table, string key, Dictionary<string, string> arguments) {
        var result = LocalizationSettings.StringDatabase.GetLocalizedString(table, key, arguments: arguments);
        if (result.StartsWith("No translation found"))
            return key;
        return result;
    }

    public void Bind(GameSettingsData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.LocalizationManager = new(DefaultValue);
        _data = data.LocalizationManager;
        UpdateValue();
    }

    public void UpdateValue() {
        LocalizationSettings.SelectedLocale = _locales[_data.LastValue];
    }
}
