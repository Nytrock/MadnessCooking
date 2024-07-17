using UnityEngine;

public class FoodRecipeTechnic : HoverTextActivator {
    [SerializeField] protected GrayscaleImageRenderer _techicIcon;

    public void SetTechnic(Technic technic, bool haveTechnic) {
        _showingMessage = technic.Name;
        _techicIcon.Setup(technic.Icon, !haveTechnic);
    }
}
