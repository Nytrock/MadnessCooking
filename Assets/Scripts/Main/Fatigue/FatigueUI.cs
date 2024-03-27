using UnityEngine;
using UnityEngine.UI;

public class FatigueUI : MonoBehaviour
{
    [SerializeField] private FatigueManager _manager;
    [SerializeField] private Slider _fatigueSlider;
    [SerializeField] private Animator _screenAnimator;

    private void Awake()
    {
        _manager.TiredChanged += ChangeTiredAnimation;
    }

    private void Start()
    {
        _fatigueSlider.maxValue = _manager.FatigueMax;
    }

    private void Update()
    {
        _fatigueSlider.value = _fatigueSlider.maxValue - _manager.FatigueNow;
    }

    private void ChangeTiredAnimation(bool newState)
    {
        _screenAnimator.SetBool("isSleep", newState);
    }
}
