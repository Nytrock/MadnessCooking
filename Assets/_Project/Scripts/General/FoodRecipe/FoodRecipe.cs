using UnityEngine;
using MadnessCooking.Kitchen;

namespace MadnessCooking.General {
    public abstract class FoodRecipe<TPart> : MonoBehaviour
        where TPart : FoodRecipePart {
        [SerializeField] protected TPart[] _recipeParts;
        [SerializeField] protected FoodRecipeTechnic _techicIcon;
        [SerializeField] protected KitchenStorage _kitchenStorage;
        [SerializeField] protected TechnicManager _technicManager;
        protected Food _food;

        public bool CanCook {
            get {
                if (!_techicIcon.IsAvailable)
                    return false;

                foreach (var part in _recipeParts)
                    if (!part.IsAvailable)
                        return false;

                return true;
            }
        }

        public virtual void SetupRecipe(Food food) {
            DisableParts();
            _food = food;

            SetupIngredients();
            SetupTechnic();
        }

        public virtual void SetHoverText(HoverTextPanel hoverText) {
            foreach (var recipePart in _recipeParts)
                recipePart.SetHoverPanel(hoverText);
            _techicIcon.SetHoverPanel(hoverText);
        }

        public virtual void DisableParts() {
            foreach (var part in _recipeParts)
                part.Disable();
            _techicIcon.ChangeState(false);
        }

        protected abstract void SetupIngredients();
        protected abstract void SetupTechnic();
    }
}
