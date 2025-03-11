using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuFoodRenderer : MonoBehaviour {
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _banishedCross;

    private MenuFood _menuFood;
    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangeBanishedState);
    }

    public void SetMenuFood(MenuFood menuFood) {
        _menuFood = menuFood;
        _icon.sprite = _menuFood.Food.Icon;
        UpdateBanishedState();
    }

    private void ChangeBanishedState() {
        _menuFood.ChangeBanishedState();
        UpdateBanishedState();
    }

    private void UpdateBanishedState() {
        _banishedCross.SetActive(_menuFood.IsBanished);
    }
}
