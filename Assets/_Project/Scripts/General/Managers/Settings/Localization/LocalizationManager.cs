using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationManager : Singleton<LocalizationManager>, IBindable<GameSettingsData>, ISettingableWithOptions {
    [SerializeField] private List<Locale> _locales;
    [SerializeField] private Locale _defaultLocale;

    private SettingsPointData<int> _data;
    private bool _isLocalesLoaded;

    public int OptionsCount => _locales.Count;
    public bool IsLocalesLoaded => _isLocalesLoaded;
    public int DefaultValue => _locales.IndexOf(_defaultLocale);

    public event Action LocalizationChanged;

    [RuntimeInitializeOnLoadMethod]
    private static void InitializeOnLoad() {
        LocalizationSettings.SelectedLocaleChanged -= UpdateLocalizationFromEditorStatic;
    }

    protected override void Awake() {
        base.Awake();
        LocalizationSettings.SelectedLocaleChanged += UpdateLocalizationFromEditorStatic;
    }

    protected IEnumerator Start() {
        _isLocalesLoaded = false;
        yield return new WaitForEndOfFrame();
        yield return LocalizationSettings.InitializationOperation;
        _isLocalesLoaded = true;
        UpdateValue();
    }

    private void UpdateLocalization() {
        if (!Application.isPlaying) return;

        LocalizationChanged?.Invoke();
    }

    public async Task<string> GetLocalization(string table, string key, Dictionary<string, string> arguments = null) {
        if (!_isLocalesLoaded)
            return string.Empty;

        if (arguments != null) {
            Dictionary<string, string> localizedArguments = new();
            foreach (var argumentKey in arguments.Keys)
                localizedArguments[argumentKey] = await GetLocalization(table, arguments[argumentKey]);
            arguments = localizedArguments;
        }

        return await LocalizationSettings.StringDatabase.GetLocalizedStringAsync(table, key, arguments: arguments).Task;
    }

    public void Bind(GameSettingsData data) {
        data.LocalizationManager ??= new(DefaultValue);
        _data = data.LocalizationManager;
    }

    public void UpdateValue() {
        if (!_isLocalesLoaded)
            return;

        LocalizationSettings.SelectedLocale = _locales[_data.LastValue];
        UpdateLocalization();
    }

    private static void UpdateLocalizationFromEditorStatic(Locale locale) {
        Instance.UpdateLocalizationFromEditor(locale);
    }

    private void UpdateLocalizationFromEditor(Locale locale) {
        int newValue = _locales.IndexOf(locale);
        _data.ChangeValue(newValue);
        _data.SubmitChanging();
        UpdateValue();
    }

    public void LateStart() { }
}
