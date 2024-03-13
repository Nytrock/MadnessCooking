public abstract class ClientState
{
    public abstract void EnterState(Client client);
    public abstract void UpdateState(Client client);
    public abstract void ExitState(Client client);
}
