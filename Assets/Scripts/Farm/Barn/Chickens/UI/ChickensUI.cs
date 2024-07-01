using UnityEngine;
using UnityEngine.UI;

public class ChickensUI : MonoBehaviour {
    [SerializeField] private Chickens _chickens;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _feedButton;
    [SerializeField] private ChickensFoodCountRenderer _countRenderer;
    [SerializeField] private Slider _eggSlider;
    [SerializeField] private ItemInfoRendererWithCount _eggRenderer;

    private void Awake() {
        _countRenderer.SetChickens(_chickens);
        _chickens.FoodCountChanged += UpdateFoodCount;
        _chickens.EggCountChanged += _eggRenderer.SetCount;
    }

    private void Start() {
        _eggRenderer.SetItemInfo(ConstIngredients.Instance.Egg);
        _eggSlider.maxValue = _chickens.EggTime;
        _panel.SetActive(false);
    }

    private void Update() {
        _eggSlider.value = _chickens.Data.NowTime;
        _eggSlider.gameObject.SetActive(_chickens.Data.IsFeed);
    }

    public void Feed() => _chickens.Feed();

    private void UpdateFoodCount(int foodCount) {
        _feedButton.interactable = foodCount > 0 || _chickens.Data.IsInfiniteFood;
        _countRenderer.UpdateFoodCount();
    }

    public void ChangeState() {
        _panel.SetActive(!_panel.activeSelf);
    }

    public void EggsToCar() => _chickens.EggsToCar();
}
