using UnityEngine;
using UnityEngine.UI;

public class FarmCarUI : IngredientStorageUI<FarmData>
{
    [SerializeField] private FarmCarWaitManager _waitManager;
    [SerializeField] private Button _sendButton;

    protected override void Awake()
    {
        _sendButton.onClick.AddListener(_waitManager.StartWait);
        _sendButton.onClick.AddListener(CarLeave);
    }

    private void Update()
    {
        _sendButton.interactable = _waitManager.Data.CarState == CarState.Calm;
    }

    private void CarLeave()
    {
        foreach(var button in _buttons)
            _buttonPool.PutObject(button);
        _buttons.Clear();
        _panel.SetActive(false);
    }
}
