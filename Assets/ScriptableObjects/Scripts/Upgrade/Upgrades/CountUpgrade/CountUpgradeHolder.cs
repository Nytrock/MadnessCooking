public class CountUpgradeHolder
{
    public readonly CountUpgrade _countUpgrade;
    public int NowCount { get; private set; }

    public CountUpgradeHolder(CountUpgrade upgrade)
    {
        _countUpgrade = upgrade;
        NowCount = 0;
    }

    public void AddCount()
    {
        NowCount++;
    }
}
