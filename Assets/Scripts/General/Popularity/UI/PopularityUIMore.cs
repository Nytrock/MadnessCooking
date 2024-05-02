using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopularityUIMore : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _popularityMultiplier;
    [SerializeField] private TextMeshProUGUI _singleChance;
    [SerializeField] private TextMeshProUGUI _doubleChance;
    [SerializeField] private TextMeshProUGUI _tripleChance;
    [SerializeField] private TextMeshProUGUI _quarterChance;

    private void Start()
    {
        ChangeMode(false);
    }

    public void ChangeMode(bool newMode)
    {
        _panel.SetActive(newMode);
    }

    public void UpdateInfo(PopularityLevel level)
    {
        _description.text = level.Description;
        _popularityMultiplier.text = level.PopularityMultiplier.ToString();
        _singleChance.text = level.SingleChance.ToString() + "%";
        _doubleChance.text = level.DoubleChance.ToString() + "%";
        _tripleChance.text = level.TripleChance.ToString() + "%";
        _quarterChance.text = level.QuarterChance.ToString() + "%";
    }
}
