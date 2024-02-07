using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoldAddUI : MonoBehaviour
{
    [SerializeField] private GameObject _UI;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TextMeshProUGUI _countText;

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

    public virtual void SetCountText(int count)
    {
        _countText.text = count.ToString();
    }

    public virtual void SetCountText(int countRaw, int countReady)
    {
        _countText.text = countReady.ToString();
    }
}
