using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CountRenderer : MonoBehaviour {
    private readonly string[] _prefixes = { "K", "M", "B" };
    private readonly string _overflowMessage = "WHAT";
    private TextMeshProUGUI _countText;

    private void GetCountText() {
        _countText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCount(int count) {
        if (_countText == null)
            GetCountText();

        if (count == int.MaxValue) {
            _countText.text = _overflowMessage;
            return;
        }

        int index = -1;
        float resCount = count;
        while (index < _prefixes.Length) {
            if (resCount < 1000f)
                break;
            resCount /= 1000f;
            index++;
        }
        if (index == -1)
            _countText.text = count.ToString();
        else if (index == _prefixes.Length)
            _countText.text = _overflowMessage;
        else
            _countText.text = $"{resCount:F2}{_prefixes[index]}";
    }

    public void ResetText() {
        _countText.text = "";
    }
}
