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
        int index = 0;
        for (int i = 0; i < _manager.UpgradesCount; i++) {
            FarmBedUpgrade upgrade = _manager.GetUpgradeByIndex(i);
            bool isAccessable = CheckUpgradeAccessable(upgrade);
            if (isAccessable) {
                var button = _choiceButtonPool.GetObject();
                button.Setup(upgrade, index, this);
                _choiceButtons.Add(button);
                index++;
            }
        }
    }

    public override void SetChoice()
    {
        base.SetChoice();
        var upgrade = _choiceButtons[_chosedIndex].Item;
        _changingBed.Upgrader.AddUpgrade(upgrade);

        int index = 0;
        foreach (FarmBedUpgrade nextUpgrade in upgrade.NextUpgrades) {
            bool isAccessable = CheckUpgradeAccessable(nextUpgrade);
            if (isAccessable) {
                var button = _choiceButtonPool.GetObject();
                button.Setup(nextUpgrade, index, this);
                _choiceButtons.Add(button);
            }
        }

        _choiceButtons[_chosedIndex].Disable();
        Deselect();
    }

    private bool CheckUpgradeAccessable(FarmBedUpgrade upgrade)
    {
        var bedType = _changingBed.BedType;
        var farmBedUpgrader = _changingBed.Upgrader;
        bool isAccessable = true;

        isAccessable &= _manager.ContainsUpgrade(upgrade);
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

        if (_chosedIndex == -1)
            return;

        var upgrade = _choiceButtons[_chosedIndex].Item;
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
