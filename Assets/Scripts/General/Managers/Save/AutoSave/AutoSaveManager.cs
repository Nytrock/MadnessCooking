using UnityEngine;

public class AutoSaveManager : MonoBehaviour {
    [SerializeField, Min(1)] private int _needAutoSaveMinutes;
    [SerializeField] private AutoSaveUI _UI;
    [SerializeField] private GameSaveManager _saveManager;
    [SerializeField] private TutorialManager _titorialManager;
    private float _nowTime;

    private float _needAutoSaveTime;
    private bool _isSaving;

    private void Awake() {
        _saveManager.SaveEnded += EndAutoSave;
        _needAutoSaveTime = 60 * _needAutoSaveMinutes;
    }

    private void Update() {
        if (_isSaving)
            return;

        if (_titorialManager.IsWork)
            return;

        if (_nowTime < _needAutoSaveTime)
            _nowTime += Time.deltaTime;
        else if (!FatigueManager.Instance.IsTired)
            StartAutoSave();
    }

    private void StartAutoSave() {
        _saveManager.Save();
        _isSaving = true;

        if (_UI != null)
            _UI.PlaySaveAnimation();
    }

    private void EndAutoSave() {
        _nowTime = 0;
        _isSaving = false;

        if (_UI != null)
            _UI.StopSaveAnimation();
    }
}