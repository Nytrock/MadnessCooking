using System.Data;
using System.Linq;
using UnityEngine;

public class FarmBedUpgradeUI : ChoiceBuyUI<FarmBedUpgrade>
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
        for (int i = 0; i < _manager.HaveUpgradesCount; i++) {
            FarmBedUpgrade upgrade = _manager.GetUpgradeByIndex(i);
            bool isAccessable = CheckUpgradeAccessable(upgrade);
            if (isAccessable) {
                var button = _choiceButtonPool.GetObject();
                button.Setup(upgrade, i, this);
                _choiceButtons.Add(button);
            }
        }
    }

    public override void SetChoice()
    {
        var upgrade = _manager.GetUpgradeByIndex(_chosedIndex);
        _changingBed.Upgrader.AddUpgrade(upgrade);

        foreach (FarmBedUpgrade nextUpgrade in upgrade.NextUpgrades) {
            bool isAccessable = CheckUpgradeAccessable(nextUpgrade);
            int i = _manager.GetIndexOfUpgrade(nextUpgrade);
            if (isAccessable && i != -1) {
                var button = _choiceButtonPool.GetObject();
                button.Setup(nextUpgrade, i, this);
                _choiceButtons.Add(button);
            }
        }

        _choiceButtons[_chosedIndex].Destroy();
        Deselect();
    }

    private bool CheckUpgradeAccessable(FarmBedUpgrade upgrade)
    {
        var bedType = _changingBed.BedType;
        var farmBedUpgrader = _changingBed.Upgrader;
        bool isAccessable = true;

        isAccessable &= upgrade.SuitableBedTypes.Contains(bedType);
        isAccessable &= !farmBedUpgrader.HaveUpgrade(upgrade);
        foreach (FarmBedUpgrade needUpgrade in upgrade.NeedUpgrades) {
            isAccessable &= farmBedUpgrader.HaveUpgrade(needUpgrade);
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

        var upgrade = _manager.GetUpgradeByIndex(_chosedIndex);
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
            button.Destroy();
        _choiceButtons.Clear();
    }
}
