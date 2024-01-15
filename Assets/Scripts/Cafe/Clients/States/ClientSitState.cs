public class ClientSitState : ClientState
{
    public override void EnterState(Client client)
    {
        client.GetComponent<ClientUI>().ChangeSliderState(false);
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }
}