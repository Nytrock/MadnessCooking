using System.Linq;
using UnityEngine;

public class FarmBedUpgraderUI : ChoiceBuyWithCameraStopUI<FarmBedUpgrade, FarmData>
{
    [SerializeField] private FarmBedUpgradeManager _manager;
    private FarmBed _changingBed;

    public void ActivateUpgradePanel(FarmBed groundBed)
    {
        Activate();
        _changingBed = groundBed;
        GenerateChoiceButtons();
    }

    protected override void GenerateChoiceButtons()
    {
        int index = 0;
        for (int i = 0; i < _manager.UpgradesCount; i++) {
            FarmBedUpgrade upgrade = _manager.GetUpgradeByIndex(i);
            bool isAccessable = CheckUpgradeAccessable(upgrade);
            if (isAccessable) {
                ChoiceBuyButton<FarmBedUpgrade> button = _choiceButtonPool.GetObject();
                button.Setup(upgrade, index, this);
                _choiceButtons.Add(button);
                index++;
            }
        }
    }

    public override void SetChoice()
    {
        base.SetChoice();
        FarmBedUpgrade upgrade = _choiceButtons[_chosedIndex].Item;
        _changingBed.AddUpgrade(upgrade);

        int index = 0;
        foreach (var nextUpgrade in upgrade.NextUpgrades) {
            var farmBedNextUpgrade = nextUpgrade as FarmBedUpgrade;
            bool isAccessable = CheckUpgradeAccessable(farmBedNextUpgrade);
            if (isAccessable) {
                var button = _choiceButtonPool.GetObject();
                button.Setup(farmBedNextUpgrade, index, this);
                _choiceButtons.Add(button);
            }
        }

        _choiceButtons[_chosedIndex].Disable();
        Deselect();
    }

    private bool CheckUpgradeAccessable(FarmBedUpgrade upgrade)
    {
        BedType bedType = _changingBed.BedData.BedType;
        bool isAccessable = true;

        isAccessable &= _manager.ContainsUpgrade(upgrade);
        isAccessable &= upgrade.SuitableBedTypes.Contains(bedType);
        isAccessable &= !_changingBed.HaveUpgrade(upgrade);
        foreach (var needUpgrade in upgrade.NeedUpgrades) {
            var needFarmBedUpgrade = needUpgrade as FarmBedUpgrade;
            isAccessable &= _changingBed.HaveUpgrade(needFarmBedUpgrade);
        }

        return isAccessable;
    }

    private void Deselect()
    {
        _choiceButtons[_chosedIndex].ChangeSelectedState();
        _chosedIndex = -1;
        _description.ChangeActive();
        _submitButton.interactable = false;
    }

    public override void Choice(int index, bool isBuyable)
    {
        base.Choice(index, isBuyable);

        if (_chosedIndex == -1)
            return;

        FarmBedUpgrade upgrade = _choiceButtons[_chosedIndex].Item;
        _description.UpdateDescription(upgrade);
    }

    protected override void SetSelectedState(int index)
    {
        _choiceButtons[index].ChangeSelectedState();
    }

    public override void Disable()
    {
        base.Disable();
        _changingBed = null;
        foreach (var button in _choiceButtons)
            _choiceButtonPool.PutObject(button);
        _choiceButtons.Clear();
    }
}
