public class LimitedComsumableUpgradeHolder
{
    public readonly LimitedComsumableUpgrade _countUpgrade;
    public int NowCount { get; private set; }

    public LimitedComsumableUpgradeHolder(LimitedComsumableUpgrade upgrade)
    {
        _countUpgrade = upgrade;
        NowCount = 0;
    }

    public void AddCount()
    {
        NowCount++;
    }
}
