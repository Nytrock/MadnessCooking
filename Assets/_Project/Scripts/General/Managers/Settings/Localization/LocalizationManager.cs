using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationManager : Singleton<LocalizationManager>, IBindable<GameSettingsData>, ISettingableWithOptions {
    [SerializeField] private List<Locale> _locales;
    [SerializeField] private Locale _defaultLocale;
    private SettingsPointData<int> _data;

    public int OptionsCount => _locales.Count;
    public int DefaultValue => _locales.IndexOf(_defaultLocale);

    public event Action LocalizationChanged;

    private void UpdateLocalization() {
        if (!Application.isPlaying) return;

        LocalizationChanged?.Invoke();
    }

    public string GetLocalization(string table, string key, Dictionary<string, string> arguments = null) {
        if (arguments != null) {
            foreach (var argumentKey in arguments.Keys)
                arguments[argumentKey] = GetLocalization(table, arguments[argumentKey]);
        }

        string result = LocalizationSettings.StringDatabase.GetLocalizedString(table, key, arguments: arguments);
        return result;
    }

    public void Bind(GameSettingsData data) {
        data.LocalizationManager ??= new(DefaultValue);
        _data = data.LocalizationManager;
    }

    public void LateStart() {
        UpdateValue();
    }

    public void UpdateValue() {
        LocalizationSettings.SelectedLocale = _locales[_data.LastValue];
        UpdateLocalization();
    }
}
