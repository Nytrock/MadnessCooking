using UnityEngine;
using UnityEngine.UI;

public class ChickensUI : MonoBehaviour, IActivable {
    [SerializeField] private Chickens _chickens;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _feedButton;
    [SerializeField] private LocalizedText _feedText;
    [SerializeField] private Slider _eggSlider;
    [SerializeField] private ItemInfoRendererWithCount _eggRenderer;

    private void Awake() {
        _chickens.FoodCountChanged += UpdateFoodCount;
        _chickens.EggCountChanged += UpdateEggCount;
        _chickens.FeedStateChanged += UpdateSlider;
    }

    private void Start() {
        _eggRenderer.SetItemInfo(ConstIngredients.Instance.Egg);
        _eggSlider.maxValue = _chickens.EggTime;
        _panel.SetActive(false);
    }

    private void Update() {
        _eggSlider.value = _chickens.Data.NowTime;
    }

    public void Feed() => _chickens.Feed();

    private void UpdateSlider() {
        _eggSlider.gameObject.SetActive(_chickens.Data.IsFeed);
    }

    private void UpdateFoodCount() {
        _feedButton.interactable = _chickens.Data.FoodCount > 0 || _chickens.Data.IsInfiniteFood;

        string foodCount = _chickens.Data.FoodCount.ToString();
        if (_chickens.Data.IsInfiniteFood)
            foodCount = "∞";
        _feedText.AddArguments("count", foodCount);
        _feedText.UpdateText();
    }

    private void UpdateEggCount() {
        _eggRenderer.SetCount(_chickens.Data.EggCount);
    }

    public void ChangeState() {
        _panel.SetActive(!_panel.activeSelf);
    }

    public void ChangeState(bool newState) {
        _panel.SetActive(newState);
    }

    public void EggsToCar() => _chickens.EggsToCar();
}
