using System.IO;
using UnityEngine;

public class FileDataService {
    private readonly JsonSerializer _serializer = new();
    private readonly string _dataPath = Application.persistentDataPath; 
    private readonly string _fileName = "save";
    private readonly string _fileExtension = "nyt";

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
