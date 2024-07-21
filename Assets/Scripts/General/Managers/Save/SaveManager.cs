using System;
using UnityEngine;

public class SaveManager : MonoBehaviour {
    private GameData _gameData;
    private FileDataService _dataService;

    [SerializeField] private UpgradeManager _upgradeManager;

    [Header("Save parts")]
    [SerializeField] private SaveGeneralManager _generalPart;
    [SerializeField] private SaveCafeManager _cafePart;
    [SerializeField] private SaveKitchenManager _kitchenPart;
    [SerializeField] private SaveFarmManager _farmPart;
    [SerializeField] private SaveOfficeManager _officePart;

    public event Action SaveEnded;

    private void Awake() {
        _dataService = new FileDataService();
        Application.targetFrameRate = 60;
    }

    private void Start() {
        Load();
    }

    [ContextMenu("Save")]
    public void Save() {
        _dataService.Save(_gameData);
        SaveEnded?.Invoke();
    }

    [ContextMenu("Load")]
    private void Load() {
        _gameData = _dataService.Load();
        bool isFileEmpty = _gameData == null;
        if (isFileEmpty)
            _gameData = new GameData();

        _generalPart.LoadData(_gameData.General, isFileEmpty);
        _cafePart.LoadData(_gameData.Cafe, isFileEmpty);
        _kitchenPart.LoadData(_gameData.Kitchen, isFileEmpty);
        _farmPart.LoadData(_gameData.Farm, isFileEmpty);
        _officePart.LoadData(_gameData.Office, isFileEmpty);
        _upgradeManager.Bind(_gameData, isFileEmpty);
    }
}
