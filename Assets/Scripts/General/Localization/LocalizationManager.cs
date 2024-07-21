using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationManager : Singleton<LocalizationManager> {
    public event Action LocalizationChanged;

    protected override void Awake() {
        base.Awake();
        LocalizationSettings.SelectedLocaleChanged += delegate { ChangeLocale(); };
    }

    private void ChangeLocale() {
        if (!Application.isPlaying) return;

        LocalizationChanged?.Invoke();
    }

    public string GetLocalization(string table, string key, Dictionary<string, string> arguments) {
        var result = LocalizationSettings.StringDatabase.GetLocalizedString(table, key, arguments: arguments);
        if (result.StartsWith("No translation found"))
            return key;
        return result;
    }
}
