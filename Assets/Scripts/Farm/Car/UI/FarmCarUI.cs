using UnityEngine;
using UnityEngine.UI;

public class FarmCarUI : IngredientStorageUI<FarmData>, IBindable<FarmData>
{
    [SerializeField] private Button _sendButton;
    private SerializableCarWaitManager _data;

    private void Update()
    {
        _sendButton.interactable = _data.CarState == CarState.Calm;
    }

    public void CarLeave()
    {
        foreach(var button in _buttons)
            _buttonPool.PutObject(button);
        _buttons.Clear();
        _panel.SetActive(false);
    }

    public void Bind(FarmData data, bool isFileEmpty)
    {
        _data = data.CarWaitManager;
    }
}
