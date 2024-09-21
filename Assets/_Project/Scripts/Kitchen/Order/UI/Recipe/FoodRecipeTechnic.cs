public class FoodRecipeTechnic : FoodRecipeAdditionalPart {
    public void SetTechnic(Technic technic, bool haveTechnic) {
        Awake();
        _showingItem = technic;
        _icon.Setup(technic.Icon, !haveTechnic);
    }
}
