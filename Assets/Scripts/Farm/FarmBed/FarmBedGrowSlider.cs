using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class FarmBedGrowSlider : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void UpdateSlider(float nowValue)
    {
        _slider.value = nowValue;
    }

    public void SetMaxTime(float maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = 0;
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
