using UnityEngine;
using UnityEngine.UI;

public class OrderCookingSlider : MonoBehaviour {
    [SerializeField] private Slider _cookingSlider;
    private TechnicManager _technicManager;
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

        _technicData = _technicManager.FindHolderByTechic(order.Food.TypeTechnic).Data;
        _cookingSlider.maxValue = _technicData.NeedWaitTime;
    }

    private void EndCook() {
        _isCooking = false;
    }

    public void SetTechnicManager(TechnicManager technicManager) {
        _technicManager = technicManager;
    }
}
