using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PopularityUIMore : MonoBehaviour
{
    [SerializeField] private Mask _mask;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _popularityMultiplier;
    [SerializeField] private TextMeshProUGUI _singleChance;
    [SerializeField] private TextMeshProUGUI _doubleChance;
    [SerializeField] private TextMeshProUGUI _tripleChance;
    [SerializeField] private TextMeshProUGUI _quarterChance;

    private Image _maskImage;

    private void Start()
    {
        _maskImage = _mask.GetComponent<Image>();
        ChangeMode(false);
    }

    public void ChangeMode(bool newMode)
    {
        _mask.enabled = !newMode;
        _maskImage.raycastTarget = !newMode;
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
