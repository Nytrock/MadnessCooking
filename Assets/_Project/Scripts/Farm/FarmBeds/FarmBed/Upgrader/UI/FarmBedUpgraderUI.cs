using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BedTypeStyleUpdater))]
public class FarmBedUpgraderUI : ChoiceBuyUI<FarmBedUpgrade> {
    [SerializeField] private FarmBedUpgradeManager _manager;

    private BedTypeStyleUpdater _styleUpdater;
    private FarmBed _changingBed;

    private void Awake() {
        _styleUpdater = GetComponent<BedTypeStyleUpdater>();
    }

    public void ActivateUpgradePanel(FarmBed groundBed) {
        Activate();
        _changingBed = groundBed;
        _styleUpdater.UpdateStyle(_changingBed.Data.BedType);
        GenerateChoiceButtons();
    }

    protected override void GenerateChoiceButtons() {
        DestoyOldButtons();

        int index = 0;
        foreach (var upgrade in _manager.GetAllUpgrades()) {
            bool isAccessable = CheckUpgradeAccessable(upgrade);
            if (isAccessable) {
                FarmBedUpgradeButton button = _choiceButtonPool.GetObject() as FarmBedUpgradeButton;
                button.Setup(upgrade, index, this);
                button.UpdateStyle(_changingBed.Data.BedType);
                _choiceButtons.Add(button);
                index++;
            }
        }
    }

    public override void SetChoice() {
        FarmBedUpgrade upgrade = _choiceButtons[_chosedIndex].Item;
        MoneyManager.Instance.ChangeMoney(-upgrade.PriceToAdd);
        FatigueManager.Instance.AddFatigue(upgrade.FatigueCoef);
        _changingBed.AddUpgrade(upgrade);

        int index = 0;
        foreach (var nextUpgrade in upgrade.NextItems) {
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

    private bool CheckUpgradeAccessable(FarmBedUpgrade upgrade) {
        BedType bedType = _changingBed.Data.BedType;
        bool isAccessable = true;

        isAccessable &= _manager.ContainsUpgrade(upgrade);
        isAccessable &= upgrade.SuitableBedTypes.Contains(bedType);
        isAccessable &= !_changingBed.HaveUpgrade(upgrade);
        foreach (var needUpgrade in upgrade.NeedItems) {
            var needFarmBedUpgrade = needUpgrade as FarmBedUpgrade;
            if (needFarmBedUpgrade == null)
                continue;

            isAccessable &= _changingBed.HaveUpgrade(needFarmBedUpgrade);
        }

        return isAccessable;
    }

    private void Deselect() {
        _choiceButtons[_chosedIndex].ChangeChoosedState();
        _chosedIndex = -1;
        _description.ChangeState();
        _submitButton.interactable = false;
    }

    public override void Choice(int index, bool isBuyable) {
        base.Choice(index, isBuyable);

        if (_chosedIndex == -1)
            return;

        FarmBedUpgrade upgrade = _choiceButtons[_chosedIndex].Item;
        _description.UpdateDescription(upgrade);
    }

    protected override void SetSelectedState(int index) {
        _choiceButtons[index].ChangeChoosedState();
    }

    public override void Disable() {
        base.Disable();
        _changingBed = null;
        foreach (var button in _choiceButtons)
            _choiceButtonPool.PutObject(button);
        _choiceButtons.Clear();
    }
}
