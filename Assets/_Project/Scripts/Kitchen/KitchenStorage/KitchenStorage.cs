using MadnessCooking.Farm;
using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public class KitchenStorage : IngredientStorage, ISaveable {
        public void LoadSave(GameData data) {
            data.Kitchen.KitchenStorage ??= new(_defaultIngredients);
            Data = data.Kitchen.KitchenStorage;
        }

        public void LateStart() {
            Data.SetMaxSpace(_defaultMaxSpace);
            InvokeLoadingDataEnded();
            foreach (var ingredientCount in Data.Ingredients)
                InvokeIngredientAdded(ingredientCount);
        }

        public void RemoveAllSpices() {
            int spiceCount = Data.GetIngredientCount(ConstIngredients.Instance.Spice);
            RemoveIngredient(ConstIngredients.Instance.Spice, spiceCount);
        }
    }
}
