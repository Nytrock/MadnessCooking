public abstract class ClientState
{
    protected Client _client;
    public abstract void EnterState(Client client);
    public abstract void UpdateState();
    public abstract void ExitState();
}
