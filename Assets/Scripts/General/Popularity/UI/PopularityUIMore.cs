using TMPro;
using UnityEngine;

public class PopularityUIMore : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _popularityMultiplierText;
    [SerializeField] private TextMeshProUGUI _singleChanceText;
    [SerializeField] private TextMeshProUGUI _doubleChanceText;
    [SerializeField] private TextMeshProUGUI _tripleChanceText;
    [SerializeField] private TextMeshProUGUI _quarterChanceText;

    private void Start() {
        ChangeMode(false);
    }

    public void ChangeMode(bool newMode) {
        _panel.SetActive(newMode);
    }

    public void UpdateInfo(PopularityLevel level) {
        _descriptionText.text = level.Description;
        _popularityMultiplierText.text = level.PopularityMultiplier.ToString();
        _singleChanceText.text = (level.SingleChance / 10.0).ToString() + "%";
        _doubleChanceText.text = (level.DoubleChance / 10.0).ToString() + "%";
        _tripleChanceText.text = (level.TripleChance / 10.0).ToString() + "%";
        _quarterChanceText.text = (level.QuarterChance / 10.0).ToString() + "%";
    }
}
