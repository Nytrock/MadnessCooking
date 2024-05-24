using System;

public class GroupClient : Client
{
    private ClientGroupHolder _table;

    public override void Setup(ClientSettings settings)
    {
        base.Setup(settings);
        if (ClientData.State == ClientState.Leave)
            return;

        CafeSpot spot = Spawner.GetSpot(SpotIndex);
        if (!spot.TryGetComponent(out _table))
            throw new ArgumentNullException("Spot doesn't have the required class ClientGroupHolder");

        _table.WaitStarted += Sit;
    }

    public override void Pay()
    {
        WaitOthers();
        _table.CheckTalk();
    }

    public override void FoodRejected()
    {
        WaitOthers();
        _table.DecreaseTalk();
        InvokeRejected();
    }

    public override void Eat()
    {
        _table.AddMoney(ClientData.Order.Food.MoneyGet);
        _table.EndlessWait();
        base.Eat();
    }

    public override void Wait()
    {
        WaitOthers();
        _table.CheckWait();
    }

    private void WaitOthers()
    {
        ClientData.State = ClientState.WaitOthers;
        ChangeState();
    }
}
