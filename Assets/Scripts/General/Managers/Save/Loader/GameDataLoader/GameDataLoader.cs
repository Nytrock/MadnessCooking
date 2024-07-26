using UnityEngine;

public class GameDataLoader : DataLoader<GameData> {
    [Header("Load parts")]
    [SerializeField] private GeneralDataLoader _generalLoader;
    [SerializeField] private CafeDataLoader _cafeLoader;
    [SerializeField] private KitchenDataLoader _kitchenLoader;
    [SerializeField] private FarmDataLoader _farmLoader;
    [SerializeField] private OfficeDataLoader _officeLoader;
    [SerializeField] private UpgradeManager _upgradeManager;

    public override void Load(GameData gameData, bool isFileEmpty) {
        _generalLoader.LoadData(gameData.General, isFileEmpty);
        _cafeLoader.LoadData(gameData.Cafe, isFileEmpty);
        _kitchenLoader.LoadData(gameData.Kitchen, isFileEmpty);
        _farmLoader.LoadData(gameData.Farm, isFileEmpty);
        _officeLoader.LoadData(gameData.Office, isFileEmpty);
        _upgradeManager.Bind(gameData, isFileEmpty);
    }
}
