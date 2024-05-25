using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ClientUI))]
public class Client : MonoBehaviour
{
    #region States Settings
    protected ClientBaseState _nowState;
    private readonly ClientWalkState _walkState = new();
    private readonly ClientWaitState _waitState = new();
    private readonly ClientEatState _eatState = new();
    private readonly ClientSitState _sitState = new();
    private readonly ClientWaitOthersState _waitOthersState = new();
    #endregion

    [SerializeField] private ClientSkin _skin;
    [SerializeField, Min(0)] private float _minWaitTime;
    [SerializeField, Min(0)] private float _maxWaitTime;

    protected ClientUI _clientUI;

    public SerializableClient ClientData { get; private set; }
    public ClientsSpawner Spawner { get; private set; }
    public bool IsEatTimeShow { get; private set; }
    public int SpotIndex { get; private set; }
    public int TableIndex { get; private set; }

    public event Action<Client> OrderActivated;
    public event Action<Client> ClientLeave;
    public event Action<Client> ClientRejected;
    public event Action<Client> ClientEat;

    private void Awake()
    {
        _clientUI = GetComponent<ClientUI>();
    }

    public void StartNewCycle()
    {
        _skin.SetSkin(ClientData.Type);
        _clientUI.StartNewCycle();
    }

    protected void ChangeState()
    {
        ClientBaseState clientState = null;
        switch (ClientData.State) {
            case ClientState.Spawn:
            case ClientState.Leave:
                clientState = _walkState;
                break;
            case ClientState.Wait: 
                clientState = _waitState;
                break;
            case ClientState.Eat: 
                clientState = _eatState;
                break;
            case ClientState.Sit: 
                clientState = _sitState;
                break;
            case ClientState.WaitOthers:
                clientState = _waitOthersState;
                break;
        }

        _nowState?.ExitState(this);
        _nowState = clientState;
        _nowState.EnterState(this);
    }

    private void Update()
    {
        _nowState.UpdateState(this);
    }

    public void RotateSkin(Direction direction)
    {
        _skin.RotateSkin(direction);
    }

    private void RotateSkin()
    {
        CafeSpot spot = Spawner.GetSpot(SpotIndex);
        _skin.RotateSkin(spot.GetSeatRotation(TableIndex));
    }

    public void TakeSeat()
    {
        RotateSkin();
        _skin.ChangeSortingLayer();
    }

    public virtual void Setup(ClientSettings settings)
    {
        Spawner = settings.Spawner;

        TableIndex = settings.TableIndex;
        SpotIndex = settings.SpotIndex;

        ClientData = settings.Data;
        if (ClientData.WaitTime == 0) {
            ClientData.WaitTime = ClientData.WaitMultiplier * Random.Range(_minWaitTime, _maxWaitTime);
        }

        transform.position = ClientData.Position.GetVector();
        _clientUI.Setup();
        ChangeState();

        if (ClientData.State != ClientState.Spawn && ClientData.State != ClientState.Leave)
            TakeSeat();
    }

    public void ActivateOrder()
    {
        OrderActivated?.Invoke(this);
        _clientUI.SetFood(ClientData.Order.Food);
        ClientData.Order.Activate();
    }

    public void CheckOrder()
    {
        if (ClientData.Order.IsFinished)
            _clientUI.ActivateYesButton();
    }

    public virtual void Wait()
    {
        ClientData.State = ClientState.Wait;
        ChangeState();
    }

    public virtual void Pay()
    {
        int moneyToPay = ClientData.Order.Food.MoneyGet;
        if (ClientData.Type == ClientType.Rich)
            moneyToPay *= 100;
        MoneyManager.Instance.ChangeMoney(moneyToPay);
        Leave();
    }

    public void Leave()
    {
        ClientLeave?.Invoke(this);
        ClientLeave = null;
        ClientEat = null;
        ClientRejected = null;
        ClientData.State = ClientState.Leave;

        ChangeState();
        _skin.ChangeSortingLayer();
        _clientUI.ChangeFoodChoiceState(false);
        _clientUI.ChangeSliderState(false);
    }

    public virtual void FoodRejected()
    {
        InvokeRejected();
        Leave();
    }

    public virtual void Eat()
    {
        ClientData.WaitTime = ClientData.Order.Food.TimeToEat * Random.Range(0.9f, 1.2f);
        ClientData.NowTime = 0;
        ClientData.State = ClientState.Eat;
        ChangeState(); 
        ClientEat?.Invoke(this);
    }

    public void Sit()
    {
        ClientData.State = ClientState.Sit;
        ChangeState();
    }

    public void Destroy()
    {
        Spawner.PutClient(this);   
    }

    public void SetSpotTableFood()
    {
        CafeSpot spot = Spawner.GetSpot(SpotIndex);
        spot.SetTableFoodSprite(ClientData.Order.Food, TableIndex);
    }

    public void ResetSpotTableFood()
    {
        CafeSpot spot = Spawner.GetSpot(SpotIndex);
        spot.ResetTableFoodSprite(TableIndex);
    }

    public void ChangeShowingTimeEat(bool value)
    {
        IsEatTimeShow = value;
    }

    protected void InvokeRejected()
    {
        ClientRejected?.Invoke(this);
    }
}