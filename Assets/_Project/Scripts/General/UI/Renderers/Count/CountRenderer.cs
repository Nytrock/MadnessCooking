using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CountRenderer : MonoBehaviour {
    private TextMeshProUGUI _countText;

    private void Awake() {
        GetCountText();
    }

    private void GetCountText() {
        _countText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCount(int count) {
        if (_countText == null)
            GetCountText();

        _countText.text = CountConverter.ToCount(count);
    }

    public void ResetText() {
        _countText.text = "";
    }
}
