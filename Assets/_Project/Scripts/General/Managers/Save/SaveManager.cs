using UnityEngine;

namespace MadnessCooking.General {
    public abstract class SaveManager<TData> : MonoBehaviour where TData : new() {
        protected TData _data;
        protected SaveFileManager<TData> _dataService;

        protected abstract string FileName { get; }

        private void Awake() {
            if (PlatformManager.IsWeb)
                _dataService = new WebSaveFileManager<TData>(FileName);
            else
                _dataService = new SaveFileManager<TData>(FileName);
        }

        private void Start() {
            Load();
        }

        public virtual void Save() {
            _dataService.Save(_data);
        }

        protected virtual void Load() {
            bool isFileEmpty = !IsDataExists();
            if (isFileEmpty)
                _data = new();
            else
                _data = _dataService.Load();
            UpdateData();
        }

        public virtual void Delete() {
            _dataService.Delete();
            _data = new();
            UpdateData();
        }

        public bool IsDataExists() {
            return _dataService.IsFileExists();
        }

        protected abstract void UpdateData();
    }
}
