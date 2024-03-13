public class ClientSitState : ClientState
{
    public override void EnterState(Client client)
    {
        client.GetComponent<ClientUI>().ChangeSliderState(false);
    }

    public override void ExitState(Client client)
    {

    }

    public override void UpdateState(Client client)
    {

    }
}