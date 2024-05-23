using UnityEngine;

[RequireComponent(typeof(SaveManager))]
public class AutoSaveManager : MonoBehaviour, IBindable<GeneralData>
{
    [SerializeField, Min(1)] private int _needAutoSaveMinutes;
    [SerializeField] private AutoSaveUI _UI;
    private SaveManager _saveManager;
    private GeneralData _data;

    private float _needAutoSaveTime;
    private bool _isSaving;

    private void Awake()
    {
        _saveManager = GetComponent<SaveManager>();
        _saveManager.SaveEnded += EndAutoSave;
    }

    private void Start()
    {
        _needAutoSaveTime = 60 * _needAutoSaveMinutes;
    }

    private void Update()
    {
        if (_isSaving)
            return;

        if (_data.AutoSaveNowTime < _needAutoSaveTime)
            _data.AutoSaveNowTime += Time.deltaTime;
        else
            StartAutoSave();
    }

    private void StartAutoSave()
    {
        _saveManager.Save();
        _isSaving = true;

        if (_UI != null)
            _UI.PlaySaveAnimation();
    }

    private void EndAutoSave()
    {
        _data.AutoSaveNowTime = 0;
        _isSaving = false;

        if (_UI != null)
            _UI.StopSaveAnimation();
    }

    public void Bind(GeneralData data, bool isFileEmpty)
    {
        _data = data;
    }
}