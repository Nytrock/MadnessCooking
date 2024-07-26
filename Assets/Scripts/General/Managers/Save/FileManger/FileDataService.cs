using System;
using System.IO;
using UnityEngine;

public class FileDataService<TData>
    where TData : ISaveable {

    private readonly JsonSerializer _serializer = new();
    private readonly string _dataPath = Application.persistentDataPath;
    private readonly string _filePath = Application.persistentDataPath;
    private readonly string _fileName;
    private const string _fileExtension = "nyt";

    public FileDataService(string fileName) {
        _fileName = fileName;
        _filePath = Path.Combine(_dataPath, string.Concat(_fileName, ".", _fileExtension));
    }

    public void Save(TData data) {
        File.WriteAllText(_filePath, _serializer.Serialize(data));
    }

    public TData Load() {
        if (!IsFileExists())
            throw new NullReferenceException($"File {_fileName} not exist.");

        return _serializer.Deserialize<TData>(File.ReadAllText(_filePath));
    }

    public void Delete() {
        if (IsFileExists())
            File.Delete(_filePath);
    }

    public bool IsFileExists() {
        return File.Exists(_filePath);
    }
}
