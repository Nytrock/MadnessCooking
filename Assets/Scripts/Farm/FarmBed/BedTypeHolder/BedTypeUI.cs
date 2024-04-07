using System;
using UnityEngine;
using UnityEngine.UI;

public class BedTypeUI : MonoBehaviour
{
    [SerializeField] private BedType _bedType;
    [SerializeField] private GameObject _UI;
    [SerializeField] private ItemInfoRendererWithCount _renderer;

    [Header("Side buttons")]
    [SerializeField] private bool _isSideButtonsWork;
    [SerializeField] private Button _pestsButton;
    [SerializeField] private Button _waterButton;
    [SerializeField] private Button _fertilizeButton;
    private bool _isWatered;
    private bool _isFertilized;

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

    public void UpdateInfo(FarmBed groundBed)
    {
        _renderer.SetItemInfo(groundBed.Ingredient);
        _renderer.SetCount(groundBed.Count);
    }

    public void CheckWater(int count)
    {
        if (!_isSideButtonsWork)
            return;

        _waterButton.interactable = count > 0 && !_isWatered;
    }

    public void CheckFertilize(int count)
    {
        if (!_isSideButtonsWork)
            return;

        _fertilizeButton.interactable = count > 0 && !_isFertilized;
    }

    public void UpdateCount(int count)
    {
        _renderer.SetCount(count);
    }

    public void UpdateSideButtons(FarmBed farmBed)
    {
        _pestsButton.interactable = !farmBed.Upgrader.IsPestsRemoved;
        if (!_isSideButtonsWork)
            return;

        _isWatered = farmBed.Upgrader.IsWatered;
        _isFertilized = farmBed.Upgrader.IsFertilized;
    }
}
