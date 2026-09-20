using MadnessCooking.General;

namespace MadnessCooking.Kitchen {
    public class FoodRecipeTechnic : FoodRecipeAdditionalPart {
        public void SetTechnic(Technic technic, bool haveTechnic) {
            if (_icon == null)
                InitializeIcon();

            _showingItem = technic;
            _isAvailable = haveTechnic;
            _grayscaleIcon.Setup(technic.Icon, !haveTechnic);
            ChangeState(true);
        }
    }
}
