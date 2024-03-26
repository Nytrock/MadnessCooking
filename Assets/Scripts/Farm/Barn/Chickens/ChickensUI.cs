using UnityEngine;
using UnityEngine.UI;

public class ChickensUI : MonoBehaviour
{
    [SerializeField] private Chickens _chickens;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _feedButton;
    [SerializeField] private ChickensFoodCountRenderer _countRenderer;
    [SerializeField] private Slider _eggSlider;
    [SerializeField] private ItemInfoRendererWithNum _eggRenderer;
    [SerializeField] private Ingredient _egg;

    private void Awake()
    {
        _chickens.EggChanged += UpdateEggCount;
        _chickens.FoodCountChanged += UpdateFoodCount;
    }

    private void Start()
    {
        _eggRenderer.SetItemInfo(_egg);
        _eggSlider.maxValue = _chickens.EggTime;
        _panel.SetActive(false);
        _countRenderer.SetChickens(_chickens);
    }

    private void Update()
    {
        _eggSlider.value = _chickens.NowTime;
        _eggSlider.gameObject.SetActive(_chickens.IsFeed);
    }

    private void UpdateEggCount()
    {
        _eggRenderer.SetNumText(_chickens.EggCount.ToString());
    }

    private void UpdateFoodCount()
    {
        _feedButton.interactable = _chickens.FoodCount > 0 || _chickens.IsInfiniteFood;
        _countRenderer.UpdateFoodCount();
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
