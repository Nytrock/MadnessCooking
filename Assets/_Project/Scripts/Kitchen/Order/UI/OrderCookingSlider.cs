using UnityEngine;
using UnityEngine.UI;

public class OrderCookingSlider : MonoBehaviour {
    [SerializeField] private Slider _cookingSlider;
    [SerializeField] private TechnicManager _technicManager;
    private bool _isCooking;

    private TechnicHolderData _technicData;

    private void Update() {
        if (!_isCooking)
            return;

        _cookingSlider.value = _technicData.NowWaitTime;
    }

    public void StartCook(Order order) {
        order.OrderFinished += EndCook;
        _isCooking = true;

        _cookingSlider.maxValue = order.Food.TimeToCook;
        _technicData = _technicManager.FindHolderByTechic(order.Food.TypeTechnic).Data;
    }

    private void EndCook() {
        _isCooking = false;
    }
}
