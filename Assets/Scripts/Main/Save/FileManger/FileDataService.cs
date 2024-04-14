using System.IO;
using UnityEngine;

public class FileDataService { 
    private JsonSerializer _serializer;
    private readonly string _dataPath; 
    private const string _fileName = "save";
    private const string _fileExtension = "nyt";

    public FileDataService(JsonSerializer serializer)
    {
        _dataPath = Application.persistentDataPath;
        _serializer = serializer;
    }

    string GetPathToFile()
    {
        return Path.Combine(_dataPath, string.Concat(_fileName, ".", _fileExtension));
    }

    public void Save(GameData data)
    {
        string fileLocation = GetPathToFile();
        File.WriteAllText(fileLocation, _serializer.Serialize(data));
    }

    public GameData Load()
    {
        string fileLocation = GetPathToFile();

        if (!File.Exists(fileLocation))
            return null;

        return _serializer.Deserialize<GameData>(File.ReadAllText(fileLocation));
    }

    public void Delete()
    {
        string fileLocation = GetPathToFile();

        if (File.Exists(fileLocation)) {
            File.Delete(fileLocation);
        }
    }
}
