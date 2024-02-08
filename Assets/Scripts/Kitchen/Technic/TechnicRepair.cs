using UnityEngine;

public class TechnicRepair : TechnicWaiter
{
    [SerializeField] private GameObject _repairSprite;

    private void Start()
    {
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
