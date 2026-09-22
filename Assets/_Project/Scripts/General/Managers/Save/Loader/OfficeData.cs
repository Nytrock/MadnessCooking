using System;
using MadnessCooking.Office;

namespace MadnessCooking.General {
    [Serializable]
    public class OfficeData {
        public OfficeUpgradeData UpgradeData { get; set; }
        public SleepBedData SleepBed { get; set; }
        public ShopData<Decor> DecorShop { get; set; }
        public ShopData<Ingredient> IngredientShop { get; set; }
        public ShopData<Food> FoodShop { get; set; }
        public ShopData<Technic> TechnicShop { get; set; }
        public ShopData<BaseUpgrade> UpgradesShop { get; set; }
        public JokesData InternetJokesData { get; set; }
    }
}
