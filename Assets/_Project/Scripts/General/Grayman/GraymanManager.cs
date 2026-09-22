using UnityEngine;

namespace MadnessCooking.General {
    public class GraymanManager : MonoBehaviour, ISaveable {
        [SerializeField] private GameSaveManager _saveManager;
        [SerializeField] private string _graymanName;
        [SerializeField] private Sprite _graymanIcon;

        private GraymanData _data;
        private BuyableItem _graymanItem;

        public bool HeWasHere => _data.HeWasHere;
        public string GraymanName => _graymanName;
        public BuyableItem GraymanItem => _graymanItem;

        public void Start() {
            _graymanItem = BuyableItem.CreateTemporaryItem<BuyableItem>(_graymanName, _graymanIcon);
        }

        public void LateStart() { }

        public void HeVisitedUs() {
            _saveManager.Save();
            _data.HeVisitedUs();
            Application.Quit();
        }

        public void LoadSave(GameData data) {
            data.General.Grayman ??= new();
            _data = data.General.Grayman;
        }
    }
}
