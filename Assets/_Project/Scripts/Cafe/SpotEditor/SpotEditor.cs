using System;
using UnityEngine;

public class SpotEditor : MonoBehaviour {
    [SerializeField] private CafeStateChanger _opener;
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private CafeSpotManager _spotManager;
    [SerializeField] private SpotPreview _preview;

    private bool _isActive = false;
    private int _newSpotIndex = 0;

    public event Action EditorActivated;
    public event Action EditorDisabled;
    public CafeSpotManager SpotManager => _spotManager;

    private void Awake() {
        _opener.CafeChanged += CheckCafeOpener;
        _locationManager.LocationChanged += delegate { ChangeWorkMode(false); };
        _spotManager.SpotsPositionChanged += _preview.Move;
        EditorDisabled += _spotManager.GenerateFreeSpotsList;
    }

    public void ChangeWorkMode() {
        _isActive = !_isActive;
        ChangeEditorState();
    }

    public void ChangeWorkMode(bool newState) {
        if (newState == _isActive)
            return;

        _isActive = newState;
        ChangeEditorState();
    }

    private void ChangeEditorState() {
        if (_isActive)
            ActivateEditor();
        else
            DisableEditor();
    }

    private void CheckCafeOpener() {
        if (_opener.IsOpened && _isActive)
            ChangeWorkMode();
    }

    private void ActivateEditor() {
        _spotManager.ActivateSpotsEditor();
        EditorActivated?.Invoke();
    }

    private void DisableEditor() {
        _spotManager.DisableSpotsEditor();
        SetPreviewIndex(-1);
        EditorDisabled?.Invoke();
    }

    public void SetPreviewIndex(int index) {
        _newSpotIndex = Mathf.Max(index, 0);
        _preview.ChangePreview(index);
    }

    public void AddNewSpot() {
        _spotManager.AddNewSpot(_newSpotIndex);
        SetPreviewIndex(-1);
        _newSpotIndex = 0;
    }
}
