using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(ClientUI))]
public class Client : MonoBehaviour
{
    #region States Settings
    protected ClientBaseState _nowState;
    private ClientWalkState _walkState = new();
    private ClientWaitState _waitState = new();
    private ClientEatState _eatState = new();
    private ClientSitState _sitState = new();
    private ClientWaitOthers _waitOthersState = new();
    #endregion

    [SerializeField] private Transform _skin;
    [SerializeField] private SortingGroup _sortingGroup;
    [SerializeField, Min(0)] private float _minWaitTime;
    [SerializeField, Min(0)] private float _maxWaitTime;

    protected ClientUI _clientUI;

    public SerializableClient ClientData { get; private set; }
    public ClientsSpawner Spawner { get; private set; }
    public bool IsEatTimeShow { get; private set; }
    public Order Order { get; private set; }
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
        SetCloth();
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

    private void SetCloth()
    {
        // Set charachter clothes
    }

    public void RotateSkin(bool isRight)
    {
        if (isRight)
            _skin.localScale = Vector2.one;
        else
            _skin.localScale = new Vector2(-1, 1);
    }

    public void TakeSeat()
    {
        RotateSkin();
        ChangeSortingGroup(5);
    }

    private void RotateSkin()
    {
        var spot = Spawner.GetSpot(SpotIndex);
        _skin.localScale = new Vector2(spot.GetSeatRotation(TableIndex), 1);
    }

    public virtual void Setup(ClientSettings settings)
    {
        Spawner = settings.Spawner;

        TableIndex = settings.TableIndex;
        SpotIndex = settings.SpotIndex;

        ClientData = settings.Data;
        if (ClientData.WaitTime == 0) {
            ClientData.WaitTime = ClientData.WaitMultiplier *
                UnityEngine.Random.Range(_minWaitTime, _maxWaitTime);
        }

        transform.position = ClientData.Position.GetVector();
        _clientUI.Setup();
        ChangeState();

        if (ClientData.State != ClientState.Spawn && ClientData.State != ClientState.Leave)
            TakeSeat();
        else
            ChangeSortingGroup(10);
    }

    public void SetOrder(Order order)
    {
        Order = order;
    }

    public void ActivateOrder()
    {
        ClientData.OrderActivated = true;
        OrderActivated?.Invoke(this);
        _clientUI.SetFood(Order.Food);
    }

    public void CheckOrder()
    {
        if (Order.IsFinished)
            _clientUI.ActivateYesButton();
    }

    public virtual void Wait()
    {
        ClientData.State = ClientState.Wait;
        ChangeState();
    }

    public virtual void Pay()
    {
        if (ClientData.Type == ClientType.Rich)
            MoneyManager.instance.ChangeMoney(Order.Food.MoneyGet * 100);
        else
            MoneyManager.instance.ChangeMoney(Order.Food.MoneyGet);
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
        ClientData.WaitTime = Order.Food.TimeToEat * UnityEngine.Random.Range(0.9f, 1.2f);
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
        var spot = Spawner.GetSpot(SpotIndex);
        spot.SetTableFoodSprite(ClientData.OrderFood, TableIndex);
    }

    public void ResetSpotTableFood()
    {
        var spot = Spawner.GetSpot(SpotIndex);
        spot.ResetTableFoodSprite(TableIndex);
    }

    public void ChangeSortingGroup(int newValue)
    {
        _sortingGroup.sortingOrder = newValue;
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
