using System;
using UnityEngine;

public abstract class SaveManager<TData> : MonoBehaviour
    where TData : ISaveable, new() {

    [SerializeField] private DataBinder<TData> _binder;

    private TData _data;
    private FileDataService<TData> _dataService;

    protected abstract string _fileName { get; }

    public event Action SaveEnded;

    private void Awake() {
        _dataService = new FileDataService<TData>(_fileName);
    }

    private void Start() {
        Load();
    }

    [ContextMenu("Save")]
    public void Save() {
        _dataService.Save(_data);
        SaveEnded?.Invoke();
    }

    private void Load() {
        if (_binder == null)
            return;

        bool isFileEmpty = !IsDataExists();
        if (isFileEmpty)
            _data = new();
        else
            _data = _dataService.Load();

        _binder.Bind(_data);
    }

    public void Delete() {
        _dataService.Delete();
    }

    public bool IsDataExists() {
        return _dataService.IsFileExists();
    }
}
