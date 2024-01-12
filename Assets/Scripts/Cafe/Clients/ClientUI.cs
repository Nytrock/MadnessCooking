using UnityEditor.Events;
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

    public Slider WaitSlider => _waitSlider;

    private void Start()
    {
        _foodImage = _mainButton.GetComponent<Image>();
        _yesButton.interactable = false;
        _buttonsBlock.SetActive(false);
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
        UnityEventTools.RemovePersistentListener(_mainButton.onClick, 0);
        _mainButton.onClick.AddListener(ChangeButtonsBlockVisible);
    }

    public void ActivateYesButton()
    {
        _yesButton.interactable = true;
    }
}
