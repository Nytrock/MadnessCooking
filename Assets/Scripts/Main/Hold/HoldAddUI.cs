using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoldAddUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private GameObject _UI;

    public void ChangeUI(bool isWork)
    {
        _UI.SetActive(isWork);
    }

    public void SetSliderMax(float progressMax)
    {
        _progressBar.maxValue = progressMax;
    }

    public void SetSliderValue(float progress)
    {
        _progressBar.value = progress;
    }

    public void SetCountText(int count)
    {
        _countText.text = count.ToString();
    }
}
