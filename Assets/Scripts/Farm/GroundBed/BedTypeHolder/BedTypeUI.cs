using UnityEngine;
using UnityEngine.UI;

public class BedTypeUI : MonoBehaviour
{
    [SerializeField] private BedType _bedType;
    [SerializeField] private GameObject _UI;
    [SerializeField] private ItemInfoRendererWithNum _renderer;

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
        _renderer.SetItemInfo(groundBed.Ingredient);
        _renderer.SetNumText(groundBed.Count.ToString());
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
        _renderer.SetNumText(count.ToString());
    }
}
