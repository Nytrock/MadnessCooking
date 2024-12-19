public class FoodRecipeTechnic : FoodRecipeAdditionalPart {
    public void SetTechnic(Technic technic, bool haveTechnic) {
        if (_icon is null)
            InitializeIcon();

        _showingItem = technic;
        _isAvailable = haveTechnic;
        _icon.Setup(technic.Icon, !haveTechnic);
        ChangeState(true);
    }
}
