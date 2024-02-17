using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OrderButton))]
public class OrderCookingSlider : MonoBehaviour
{
    [SerializeField] private Slider _cookingSlider;
    private bool _isCooking;

    private TechnicManager _technicManager;
    private TechnicHolder _technic;
    private TechnicCooker _cooker;

    private void Update()
    {
        if (!_isCooking)
            return;

        if (!_technic.IsFree) {
            _cookingSlider.value = _cooker.NowTime;
        } else {
            _isCooking = false;
            _technic = null;
        }
    }

    public void StartCook(Order order) {
        _technic = _technicManager.FindHolderByTechic(order.Food.TypeTechnic);
        _cooker = _technic.GetComponent<TechnicCooker>();
        _cookingSlider.maxValue = order.Food.TimeToCook;
        _isCooking = true;
    }

    public void SetTechnicManager(TechnicManager technicManager)
    {
        _technicManager = technicManager;
    }
}
