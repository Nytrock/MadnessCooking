using UnityEngine;

public class GraymanManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField] private GameSaveManager _saveManager;
    [SerializeField] private string _graymanName;
    [SerializeField] private Sprite _graymanIcon;

    private GraymanData _data;
    private BuyableItem _graymanItem;

    public bool HeWasHere => _data.HeWasHere;
    public BuyableItem GraymanItem => _graymanItem;

    private void Awake() {
        _graymanItem = BuyableItem.CreateTemporaryItem<BuyableItem>(_graymanName, _graymanIcon);
    }

    public void LateStart() { }

    public void HeVisitedUs() {
        _saveManager.Save();
        _data.HeVisitedUs();
        Application.Quit();
    }

    public void Bind(GeneralData data) {
        data.Grayman ??= new();
        _data = data.Grayman;
    }
}
