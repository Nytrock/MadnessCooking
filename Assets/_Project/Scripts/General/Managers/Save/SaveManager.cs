using UnityEngine;

public abstract class SaveManager<TData> : MonoBehaviour
    where TData : ISaveable, new() {

    [SerializeField] private DataBinder<TData> _binder;

    private TData _data;
    protected SaveFileManager<TData> _dataService;

    protected abstract string _fileName { get; }

    private void Awake() {
        if (PlatformManager.IsWeb)
            _dataService = new WebSaveFileManager<TData>(_fileName);
        else
            _dataService = new SaveFileManager<TData>(_fileName);
    }

    private void Start() {
        Load();
    }

    public void Save() {
        _dataService.Save(_data);
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
        _data = new();
        _binder.Bind(_data);
    }

    public bool IsDataExists() {
        return _dataService.IsFileExists();
    }
}
