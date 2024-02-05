public class TechnicRepair : TechnicWaiter
{
    protected override void EndWait()
    {
        _holder.StopRepair();
        base.EndWait();
    }
}
