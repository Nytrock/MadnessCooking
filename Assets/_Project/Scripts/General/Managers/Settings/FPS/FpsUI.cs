using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FpsUI : MonoBehaviour, IBindable<GameSettingsData>, ISettingable<bool> {
    [SerializeField] private FpsManager _manager;
    [SerializeField] private bool _defaultShow;
    private TextMeshProUGUI _text;
    private SettingsPointData<bool> _data;

    public bool DefaultValue => _defaultShow;

    private void Awake() {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        _text.text = $"{_manager.GetFPS():F0} fps";
    }

    public void Bind(GameSettingsData data) {
        data.FpsManager ??= new(DefaultValue);
        _data = data.FpsManager;
    }

    public void LateStart() {
        UpdateValue();
    }

    public void UpdateValue() {
        gameObject.SetActive(_data.LastValue);
    }
}
