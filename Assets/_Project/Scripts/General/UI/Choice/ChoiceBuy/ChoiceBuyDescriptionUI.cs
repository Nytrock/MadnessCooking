using UnityEngine;

namespace MadnessCooking.General {
    public class ChoiceBuyDescriptionUI : MonoBehaviour {
        [SerializeField] private ItemInfoRendererWithPrice _renderer;
        [SerializeField] private string _buyDescription;
        [SerializeField] private string _freeDescription;

        public void UpdateDescription(BuyableItem item) {
            _renderer.SetItemInfo(item);

            int price = item.Price;
            if (item as FarmBedUpgrade != null)
                price = (item as FarmBedUpgrade).PriceToAdd;

            if (price > 0)
                _renderer.SetPrice(_buyDescription, price);
            else
                _renderer.SetPrice(_freeDescription);
        }

        public void ChangeState(bool newState) {
            gameObject.SetActive(newState);
            if (!newState)
                _renderer.ResetInfo();
        }
    }
}
