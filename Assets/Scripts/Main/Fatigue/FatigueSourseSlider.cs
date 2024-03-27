using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class FatigueSourseSlider : MonoBehaviour
{
    [SerializeField, Min(0)] private float _fatigueCoef;
    private float _lastValue;

    private void Start()
    {
        Slider slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(IncreaseFatigue);
        _lastValue = slider.value;
    }

    private void IncreaseFatigue(float value)
    {
        FatigueManager.instance.ChangeFatigue(Mathf.Abs(_lastValue - value) * _fatigueCoef);
    }
}
