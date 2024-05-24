using UnityEngine;

public class TechnicRepairer : TechnicWaiter
{
    [SerializeField] private GameObject _repairSprite;

    protected override void Awake()
    {
        base.Awake();
        _repairSprite.SetActive(false);
    }

    public override void StartWork(float needTime)
    {
        _repairSprite.SetActive(true);
        base.StartWork(needTime);
    }
    protected override void EndWork()
    {
        _repairSprite.SetActive(false);
        _holder.StopRepair();
        base.EndWork();
    }
}
