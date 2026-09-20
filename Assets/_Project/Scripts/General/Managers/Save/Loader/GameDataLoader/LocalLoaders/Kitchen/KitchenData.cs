using System;
using MadnessCooking.Kitchen;

namespace MadnessCooking.General {
    [Serializable]
    public class KitchenData : ISaveable {
        public KitchenUpgradeData UpgradeData { get; set; }
        public FoodManagerData FoodManager { get; set; }
        public IngredientStorageData KitchenStorage { get; set; }
        public BuyableItemManagerData<Technic> TechnicManager { get; set; }
        public TechnicHolderData[] TechnicHolders { get; set; }
        public KitchenCatData Cat { get; set; }
    }
}
