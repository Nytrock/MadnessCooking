using UnityEngine;
using MadnessCooking.Farm;
using MadnessCooking.General;
using MadnessCooking.Kitchen;

namespace MadnessCooking.Office {
    public class ItemsListManager : MonoBehaviour {
        [SerializeField] private GameDataBinder _binder;
        [SerializeField] private ItemsListRenderer _renderer;
        [SerializeField] private IngredientManager _ingredientManager;
        [SerializeField] private TechnicManager _technicManager;
        [SerializeField] private FoodManager _foodManager;
        [SerializeField] private UpgradeManager _upgradeManager;
        [SerializeField] private BedTypeManager _bedTypeManager;
        [SerializeField] private DecorManager _decorManager;
        [SerializeField] private GraymanManager _graymanManager;

        private void Awake() {
            _binder.BeforeLateStart += SetupItemManagers;
        }

        private void SetupItemManagers() {
            SetupItemManager(_ingredientManager);
            SetupItemManager(_technicManager);
            SetupItemManager(_foodManager);
            SetupItemManager(_upgradeManager);
            SetupItemManager(_bedTypeManager);
            SetupItemManager(_decorManager);

            if (_graymanManager.HeWasHere)
                _renderer.CreateGraymanCategory(_graymanManager);
        }

        private void SetupItemManager<TItem>(BuyableItemManager<TItem> itemManager)
            where TItem : BuyableItem {
            _renderer.CreateCategory(itemManager);
        }
    }
}
