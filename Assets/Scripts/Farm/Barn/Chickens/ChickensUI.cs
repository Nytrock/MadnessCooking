using UnityEngine;
using UnityEngine.UI;

public class ChickensUI : MonoBehaviour
{
    [SerializeField] private Chickens _chickens;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Slider _eggSlider;
    [SerializeField] private ItemInfoRendererWithNum _eggRenderer;
    [SerializeField] private Ingredient _egg;

    private void Start()
    {
        _chickens.EggChanged += UpdateCount;
        _eggRenderer.SetItemInfo(_egg);
        _eggSlider.maxValue = _chickens.EggTime;
        _panel.SetActive(false);
    }

    private void Update()
    {
        _eggSlider.value = _chickens.NowTime;
    }

    private void UpdateCount(int count)
    {
        _eggRenderer.SetNumText(count.ToString());
    }

    public void ChangeState()
    {
        _panel.SetActive(!_panel.activeSelf);
    }

    public void EggsToCar()
    {
        _chickens.EggsToCar(_egg);
    }
}
