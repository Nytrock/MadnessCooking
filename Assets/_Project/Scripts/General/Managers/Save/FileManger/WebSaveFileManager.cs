using System;
using UnityEngine;

public class WebSaveFileManager<TData> : SaveFileManager<TData> where TData : ISaveable {
    private readonly string _prefsFilePath;

    public WebSaveFileManager(string fileName) : base(fileName) {
        _prefsFilePath = string.Concat(_fileName, ".", _fileExtension);
    }

    public override void Save(TData data) {
        string serializedData = _serializer.SerializeCoded(data);
        PlayerPrefs.SetString(_prefsFilePath, serializedData);
        PlayerPrefs.Save();
    }

    public override TData Load() {
        if (!IsFileExists())
            throw new NullReferenceException($"File {_fileName} not exist.");

        string serializedData = PlayerPrefs.GetString(_prefsFilePath);
        return _serializer.DeserializeCoded<TData>(serializedData);
    }

    public override void Delete() {
        if (!IsFileExists())
            return;

        PlayerPrefs.DeleteKey(_prefsFilePath);
    }

    public override bool IsFileExists() {
        return PlayerPrefs.HasKey(_prefsFilePath);
    }
}
