using System;
using UnityEngine;

public class AutoSaveManager : MonoBehaviour {
    [SerializeField, Min(1)] private int _needAutoSaveMinutes;
    [SerializeField] private GameSaveManager _saveManager;
    [SerializeField] private TutorialManager _tutorialManager;

    private float _nowTime;
    private float _needAutoSaveTime;
    private bool _isSaving;

    public event Action<bool> SaveChanged;

    private void Awake() {
        _saveManager.SaveEnded += EndAutoSave;
        _needAutoSaveTime = 60 * _needAutoSaveMinutes;
    }

    private void Update() {
        if (_isSaving)
            return;

        if (_tutorialManager.IsWork)
            return;

        if (_nowTime < _needAutoSaveTime)
            _nowTime += Time.deltaTime;
        else if (!FatigueManager.Instance.IsTired)
            StartAutoSave();
    }

    private void StartAutoSave() {
        _isSaving = true;
        SaveChanged?.Invoke(_isSaving);
        _saveManager.Save();
    }

    private void EndAutoSave() {
        _isSaving = false;
        SaveChanged?.Invoke(_isSaving);
        _nowTime = 0;
    }
}