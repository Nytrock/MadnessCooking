public class TechnicCooker : TechnicWaiter
{
    protected override void EndWait()
    {
        _holder.StopCook();
        base.EndWait();
    }
}
