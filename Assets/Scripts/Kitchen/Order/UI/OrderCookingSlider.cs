using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OrderButton))]
public class OrderCookingSlider : MonoBehaviour
{
    [SerializeField] private Slider _cookingSlider;
    private OrderButton _cookButton;
    private float _nowTime;
    private float _cookTime;
    private bool _isCooking;

    private void Awake()
    {
        _cookButton = GetComponent<OrderButton>();
    }

    private void Update()
    {
        if (!_isCooking)
            return;

        if (_nowTime < _cookTime) {
            _nowTime += Time.deltaTime;
            _cookingSlider.value = _nowTime;
        } else {
            _isCooking = false;
            _cookButton.FinishCook();
        }
    }

    public void StartCook(Order order) {
        _nowTime = 0;
        _cookTime = order.Food.TimeToCook;
        _cookingSlider.maxValue = _cookTime;
        _isCooking = true;
    }
}
