public abstract class ClientState
{
    protected Client client;
    public abstract void EnterState(Client client);
    public abstract void UpdateState();
    public abstract void ExitState();
}
