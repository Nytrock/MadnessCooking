using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private GameData _gameData;
    private FileDataService _dataService;

    [Header("Save parts")]
    [SerializeField] private SaveMainManager _mainPart;
    [SerializeField] private SaveCafeManager _cafePart;
    [SerializeField] private SaveKitchenManager _kitchenPart;
    [SerializeField] private SaveFarmManager _farmPart;
    [SerializeField] private SaveOfficeManager _officePart;

    private void Awake()
    {
        _dataService = new FileDataService(new JsonSerializer());
        Application.targetFrameRate = 60;

        instance = this;
        LoadAll();
    }

    [ContextMenu("Save")]
    public void SaveAll() => _dataService.Save(_gameData);

    [ContextMenu("Load")]
    private void LoadAll()
    {
        _gameData = _dataService.Load();
        if (_gameData == null) {
            _gameData = new();
            _mainPart.SetData(_gameData.Main);
            _cafePart.SetData(_gameData.Cafe);
            _kitchenPart.SetData(_gameData.Kitchen);
            _farmPart.SetData(_gameData.Farm);
            _officePart.SetData(_gameData.Office);
        } else {
            _mainPart.LoadData(_gameData.Main);
            _cafePart.LoadData(_gameData.Cafe);
            _kitchenPart.LoadData(_gameData.Kitchen);
            _farmPart.LoadData(_gameData.Farm);
            _officePart.LoadData(_gameData.Office);
        }
    }
}
