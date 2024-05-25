using UnityEngine;
using UnityEngine.UI;

public class OrderCookingSlider : MonoBehaviour
{
    [SerializeField] private Slider _cookingSlider;
    private bool _isCooking;

    private TechnicManager _technicManager;
    private TechnicCooker _cooker;

    private void Update()
    {
        if (!_isCooking)
            return;

        _cookingSlider.value = _cooker.NowTime;
    }

    public void StartCook(Order order) {
        order.OrderFinished += EndCook;
        _isCooking = true;

        _cookingSlider.maxValue = order.Food.TimeToCook;
        _cooker = _technicManager.FindHolderByTechic(order.Food.TypeTechnic).GetComponent<TechnicCooker>();
    }

    public void SetTechnicManager(TechnicManager technicManager)
    {
        _technicManager = technicManager;
    }

    private void EndCook() {
        _isCooking = false;
    }
}
