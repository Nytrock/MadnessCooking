using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BedTypeStyleUpdater))]
public class FarmBedUpgraderUI : ChoiceBuyUI<FarmBedUpgrade, FarmBedUpgradeButton> {
    [SerializeField] private FarmBedUpgradeManager _manager;

    private BedTypeStyleUpdater _styleUpdater;
    private FarmBed _changingBed;

    protected override void Awake() {
        base.Awake();
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
        base.GenerateChoiceButtons();
    }

    protected override FarmBedUpgradeButton GenerateChoiceButton(FarmBedUpgrade item) {
        bool isAccessable = CheckUpgradeAccessable(item);
        if (!isAccessable)
            return null;

        FarmBedUpgradeButton button = base.GenerateChoiceButton(item);
        button.UpdateStyle(_changingBed.Data.BedType);
        return button;
    }

    protected override IEnumerable<FarmBedUpgrade> GetItems() {
        return _manager.GetAvailableUpgrades();
    }

    public override void SubmitChoice() {
        FarmBedUpgrade upgrade = _choosedButton.Item;
        MoneyManager.Instance.ChangeMoney(-upgrade.PriceToAdd);
        FatigueManager.Instance.AddFatigue(upgrade.FatigueCoef);

        _changingBed.AddUpgrade(upgrade);
        _choiceButtonPool.PutObject(_choosedButton);

        foreach (var nextUpgrade in upgrade.NextItems)
            GenerateChoiceButton(nextUpgrade as FarmBedUpgrade);
        SelectButton(_choosedButton);
    }

    private bool CheckUpgradeAccessable(FarmBedUpgrade upgrade) {
        BedType bedType = _changingBed.Data.BedType;
        bool isAccessable = true;

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

    public override void Disable() {
        base.Disable();
        _changingBed = null;
        DestoyOldButtons();
    }
}
