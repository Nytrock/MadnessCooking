using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BedTypeUI : MonoBehaviour
{
    [SerializeField] private BedType _bedType;
    [SerializeField] private GameObject _UI;
    [SerializeField] private ItemInfoRenderer _shower;

    [Header("Side buttons")]
    [SerializeField] private bool _isSideButtonsWork;
    [SerializeField] private Button _waterButton;
    [SerializeField] private Button _fertilizeButton;

    public BedType BedType => _bedType;

    public bool IsSideButtonsWork => _isSideButtonsWork;

    private void Start()
    {
        _UI.SetActive(false);
    }

    public void ChangeMode()
    {
        _UI.SetActive(!_UI.activeSelf);
    }

    public void ChangeMode(bool newValue)
    {
        _UI.SetActive(newValue);
    }

    public void UpdateInfo(GroundBed groundBed)
    {
        _shower.SetItemInfo(groundBed.Ingredient);
        _shower.SetCountText(groundBed.Count.ToString());
    }

    public void CheckWater(int count)
    {
        if (!_isSideButtonsWork)
            return;

        _waterButton.interactable = count > 0;
    }

    public void CheckFertilize(int count)
    {
        if (!_isSideButtonsWork)
            return;

        _fertilizeButton.interactable = count > 0;
    }

    public void UpdateCount(int count)
    {
        _shower.SetCountText(count.ToString());
    }
}
