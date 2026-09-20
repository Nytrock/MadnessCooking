using System;
using System.IO;
using UnityEngine;

namespace MadnessCooking.General {
    public class SaveFileManager<TData> where TData : ISaveable {
        protected readonly JsonSerializer _serializer = new();
        private readonly string _dataPath = Application.persistentDataPath;

        protected readonly string _filePath;
        protected readonly string _fileName;
        protected const string _fileExtension = "nyt";

        public SaveFileManager(string fileName) {
            _fileName = fileName;
            if (_fileName.Contains("/"))
                FractionFileName();

            string filePath = string.Concat(_fileName, ".", _fileExtension);
            _filePath = _dataPath + "/" + filePath;
        }

        private void FractionFileName() {
            string[] folders = _fileName.Split('/')[..^1];
            string path = _dataPath;
            foreach (var folder in folders) {
                FoldersUtility.CreateFolder(path, folder);
                path += "/" + folder;
            }
        }

        public virtual void Save(TData data) {
            string serializedData = _serializer.SerializeCoded(data);
            File.WriteAllText(_filePath, serializedData);
        }

        public virtual TData Load() {
            if (!IsFileExists())
                throw new NullReferenceException($"File {_fileName} not exist.");

            string serializedData = File.ReadAllText(_filePath);
            return _serializer.DeserializeCoded<TData>(serializedData);
        }

        public virtual void Delete() {
            if (!IsFileExists())
                return;

            File.Delete(_filePath);
        }

        public virtual bool IsFileExists() {
            return File.Exists(_filePath);
        }
    }
}
