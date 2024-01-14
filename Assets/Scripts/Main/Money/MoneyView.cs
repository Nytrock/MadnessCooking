using UnityEngine;
using TMPro;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private MoneyManager _moneyManager;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _moneyManager.moneyChanged += UpdateText;
        UpdateText();
    }

    private void UpdateText()
    {
        string newText;
        int money = _moneyManager.MoneyAmount;
        if (money / 1000000000 > 0)
            newText = $"{money / 100000000f:F2}B";
        else if (money / 1000000 > 0)
            newText = $"{money / 1000000f:F2}M";
        else if (money / 1000 > 0)
            newText = $"{money / 1000f:F2}K";
        else
            newText = money.ToString();
        _text.text = newText;
    }
}
