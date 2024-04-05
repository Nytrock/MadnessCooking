using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CountRenderer : MonoBehaviour
{
    private readonly string[] _prefixes = { "K", "M", "B" };
    private const string _overflowMessage = "WAIT, WHAT?!";

    private TextMeshProUGUI _count;

    private void GetCountText()
    {
        _count = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCount(int count)
    {
        if (_count == null)
            GetCountText();

        if (count == int.MaxValue) {
            _count.text = _overflowMessage;
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
            _count.text = count.ToString();
        else if (index == _prefixes.Length)
            _count.text = _overflowMessage;
        else
            _count.text = $"{resCount:F2}{_prefixes[index]}";
    }
    
    public void ResetText()
    {
        _count.text = "";
    }
}
