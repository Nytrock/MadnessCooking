using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoldAddUI : MonoBehaviour
{
    [SerializeField] private GameObject _UI;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TextMeshProUGUI _readyCount;

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
        _readyCount.text = count.ToString();
    }

    public virtual void SetCountText(int countRaw, int countReady)
    {
        _readyCount.text = countReady.ToString();
    }
}
