public class TechnicCooker : TechnicWaiter
{
    protected override void EndWork()
    {
        _holder.StopCook();
        base.EndWork();
    }
}
