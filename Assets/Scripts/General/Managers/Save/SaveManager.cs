using System;
using UnityEngine;

public abstract class SaveManager<TData> : MonoBehaviour
    where TData : ISaveable, new() {

    [SerializeField] private DataLoader<TData> _loader;

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
        if (_loader == null)
            return;

        bool isFileEmpty = !IsDataExists();
        if (isFileEmpty)
            _data = new();
        else
            _data = _dataService.Load();

        _loader.Load(_data, isFileEmpty);
    }

    public void Delete() {
        _dataService.Delete();
    }

    public bool IsDataExists() {
        return _dataService.IsFileExists();
    }
}
