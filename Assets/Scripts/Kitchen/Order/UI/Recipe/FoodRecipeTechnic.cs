public class FoodRecipeTechnic : FoodRecipeAdditionalPart {
    public void SetTechnic(Technic technic, bool haveTechnic) {
        Awake();
        _showingMessage = technic.Name;
        _icon.Setup(technic.Icon, !haveTechnic);
    }
}
