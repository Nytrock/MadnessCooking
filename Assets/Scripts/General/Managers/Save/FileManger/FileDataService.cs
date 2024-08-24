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
        if (_fileName.Contains("/"))
            FractionFileName();

        _filePath = Path.Combine(_dataPath, string.Concat(_fileName, ".", _fileExtension));
    }

    private void FractionFileName() {
        string[] folders = _fileName.Split('/')[..^1];
        string path = _dataPath;
        foreach (var folder in folders) {
            FoldersUtility.CreateFolder(path, folder);
            path += "/" + folder;
        }
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
        FoldersUtility.DeleteFolder(_filePath);
    }

    public bool IsFileExists() {
        return File.Exists(_filePath);
    }
}
