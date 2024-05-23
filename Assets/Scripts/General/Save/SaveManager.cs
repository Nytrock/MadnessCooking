using System;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private GameData _gameData;
    private FileDataService _dataService;

    [Header("Save parts")]
    [SerializeField] private SaveGeneralManager _mainPart;
    [SerializeField] private SaveCafeManager _cafePart;
    [SerializeField] private SaveKitchenManager _kitchenPart;
    [SerializeField] private SaveFarmManager _farmPart;
    [SerializeField] private SaveOfficeManager _officePart;

    public event Action SaveEnded;

    private void Awake()
    {
        _dataService = new FileDataService(new JsonSerializer());
        Application.targetFrameRate = 60;

        instance = this;
    }

    private void Start()
    {
        Load();
    }

    [ContextMenu("Save")]
    public void Save()
    {
        _dataService.Save(_gameData);
        SaveEnded?.Invoke();
    }

    [ContextMenu("Load")]
    private void Load()
    {
        _gameData = _dataService.Load();
        bool isFileEmpty = _gameData == null;
        if (isFileEmpty) {
            _gameData = new GameData();
        }

        _mainPart.LoadData(_gameData.Main, isFileEmpty);
        _cafePart.LoadData(_gameData.Cafe, isFileEmpty);
        _kitchenPart.LoadData(_gameData.Kitchen, isFileEmpty);
        _farmPart.LoadData(_gameData.Farm, isFileEmpty);
        _officePart.LoadData(_gameData.Office, isFileEmpty);
    }
}
