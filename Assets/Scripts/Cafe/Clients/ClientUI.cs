using UnityEngine;
using UnityEngine.UI;

public class ClientUI : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _buttonsBlock;
    [SerializeField] private Button _mainButton;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Slider _waitSlider;
    
    private Image _foodImage;
    private Client _client;

    public Slider WaitSlider => _waitSlider;

    private void Start()
    {
        _foodImage = _mainButton.GetComponent<Image>();
        _client = GetComponent<Client>();
        StartNewCycle();
    }

    public void StartNewCycle()
    {
        _waitSlider.value = 0;
        _yesButton.interactable = false;
        _buttonsBlock.SetActive(false);
        _mainButton.onClick.RemoveAllListeners();
        _mainButton.onClick.AddListener(_client.ActivateOrder);
        ChangeSliderState(true);
        SetUIVisible(false);
    }

    public void SetUIVisible(bool newValue)
    {
        _canvas.SetActive(newValue);
    }

    private void ChangeButtonsBlockVisible()
    {
        _buttonsBlock.SetActive(!_buttonsBlock.activeSelf);
    }

    public void SetFood(Food food)
    {
        _foodImage.sprite = food.FoodSprite;
        _mainButton.onClick.RemoveAllListeners();
        _mainButton.onClick.AddListener(ChangeButtonsBlockVisible);
    }

    public void ActivateYesButton()
    {
        _yesButton.interactable = true;
    }

    public void ChangeSliderState(bool newValue)
    {
        _waitSlider.gameObject.SetActive(newValue);
    }
}
