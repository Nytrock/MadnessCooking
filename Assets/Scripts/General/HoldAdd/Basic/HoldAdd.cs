using UnityEngine;

public abstract class HoldAdd : MonoBehaviour, IUpgradeable, IBindable<FarmData>
{
    [Header("Upgades")]
    [SerializeField] protected BaseUpgrade _unlockUpgrade;
    [SerializeField] protected CoefficientUpgrade _autoWorkUpgrade;

    [Header("Main")]
    [SerializeField] protected HoldAddUI _holdUI;
    [SerializeField, Min(0)] private float _timeWait;
    [SerializeField, Min(0)] private float _fatigueCoef;

    protected bool _isWork;

    public HoldAddData HoldData { get; protected set; }

    public float TimeWait => _timeWait;

    protected virtual void LateStart()
    {
        ResetAll();
        UpdateUpgrades();
    }

    private void ResetAll()
    {
        _isWork = false;
        _holdUI.Setup(this);
        _holdUI.ChangeUI(_isWork);

        if (!HoldData.IsAuto)
            HoldData.NowTime = 0;
    }

    private void UpdateUpgrades()
    {
        gameObject.SetActive(HoldData.IsUnlocked);
        if (HoldData.IsAuto)
            HoldData.Speed = _autoWorkUpgrade.Coefficient;
    }

    public virtual void ChangeWorkMode(bool newValue)
    {
        _holdUI.ChangeUI(newValue);
        if (HoldData.IsAuto)
            return;

        _isWork = newValue;
        if (!_isWork)
            HoldData.NowTime = 0;
    }

    private void Update()
    {
        if (!_isWork && !HoldData.IsAuto)
            return;

        UpdateTimer();
    }

    protected virtual void UpdateTimer()
    {
        if (!HoldData.IsAuto)
            FatigueManager.Instance.ChangeFatigue(_fatigueCoef);

        if (HoldData.NowTime < _timeWait)
            HoldData.NowTime += Time.deltaTime * HoldData.Speed;
        else
            Add();
    }

    protected virtual void Add()
    {
        HoldData.NowTime = 0;
        HoldData.ReadyCount++;
    }

    public virtual void CheckUpgrade(BaseUpgrade upgrade)
    {
        if (upgrade == _unlockUpgrade)
            HoldData.IsUnlocked = true;
        else if (upgrade == _autoWorkUpgrade)
            HoldData.IsAuto = true;

        UpdateUpgrades();
    }

    public virtual void Bind(FarmData data, bool isFileEmpty)
    {
        LateStart();
    }
}
