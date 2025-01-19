using UnityEngine;
using UnityEngine.UI;

public class AnisManager : MonoBehaviour, IBindable<OfficeData> {
    [SerializeField] private SpriteChanger[] _sprites;
    [SerializeField] private Toggle _toggle;
    private JokesData _data;

    public void LateStart() {
        UpdateAnisState();
        SetupToggle();
    }

    private void SetupToggle() {
        _toggle.isOn = _data.IsAnis;
        _toggle.onValueChanged.AddListener(ChangeAnisState);
    }

    public void Bind(OfficeData data) {
        data.InternetJokesData ??= new();
        _data = data.InternetJokesData;
    }

    private void ChangeAnisState(bool isAnis) {
        _data.ChangeAnisState(isAnis);
        UpdateAnisState();
    }

    public void UpdateAnisState() {
        foreach (var sprite in _sprites)
            sprite.ChangeState(_data.IsAnis);
    }

    [ContextMenu("SetAnis")]
    public void SetAnisState() {
        foreach (var sprite in _sprites)
            sprite.ChangeState(true);
    }

    [ContextMenu("SetDefault")]
    public void SetDefaultState() {
        foreach (var sprite in _sprites)
            sprite.ChangeState(false);
    }
}
