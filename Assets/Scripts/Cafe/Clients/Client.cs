using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(ClientUI))]
public class Client : MonoBehaviour
{
    #region States Settings
    private ClientState _nowState;
    private ClientWalkState _walkState = new();
    private ClientWaitState _waitState = new();
    private ClientEatingState _eatState = new();
    private ClientSitState _sitState = new();
    #endregion

    [SerializeField] private Transform _skin;
    [SerializeField] private SortingGroup _sortingGroup;
    [SerializeField] private float _minWaitTime;
    [SerializeField] private float _maxWaitTime;

    private ClientUI _clientUI;
    private ClientsPool _pool;
    private ClientGroupHolder _table;

    public Transform EnterTarget { get; private set; }
    public Transform ExitTarget { get; private set; }
    public float WaitMultiplier { get; private set; }
    public bool IsLeaving { get; private set; }
    public bool IsEat { get; private set; }
    public bool IsEatTimeShow { get; private set; }
    public Order Order { get; private set; }
    public int TableIndex { get; private set; }
    public CafeSpot Spot { get; private set; }
    public ClientType ClientType { get; private set; }

    public event Action<Client> OrderActivated;
    public event Action<Client> ClientLeave;
    public event Action<Client> ClientEat;

    private void Awake()
    {
        _clientUI = GetComponent<ClientUI>();
        enabled = false;
    }

    public void StartNewCycle()
    {
        enabled = true;
        IsLeaving = false;
        IsEat = false;
        gameObject.SetActive(true);
        ChangeState(_walkState);
        SetCloth(); 
        _clientUI.StartNewCycle();
    }

    private void ChangeState(ClientState newState)
    {
        if (_nowState != null)
            _nowState.ExitState(this);
        _nowState = newState;
        _nowState.EnterState(this);
    }

    private void Update()
    {
        _nowState.UpdateState(this);
    }

    private void SetCloth()
    {
        // Ставим скин персонажа
    }

    public void RotateSkin(bool isRight)
    {
        if (isRight)
            _skin.localScale = Vector2.one;
        else
            _skin.localScale = new Vector2(-1, 1);
    }

    public void RotateSkin()
    {
        _skin.localScale = new Vector2(Spot.GetSeatRotation(TableIndex), 1);
    }

    public void Setup(ClientSettings settings)
    {
        EnterTarget = settings.EnterTarget;
        ExitTarget = settings.ExitTarget;
        transform.position = ExitTarget.position;

        ClientType = settings.ClientType;
        WaitMultiplier = settings.WaitMultiplier;
        _pool = settings.Pool;

        Spot = settings.Spot;
        if (InGroup())
            _table = Spot.GetComponent<ClientGroupHolder>();
        TableIndex = settings.SpotIndex;
    }

    public void SetOrder(Order order)
    {
        Order = order;
    }

    public void ActivateOrder()
    {
        OrderActivated?.Invoke(this);
        _clientUI.SetFood(Order.Food);
    }

    public void CheckOrder()
    {
        if (Order.IsFinished)
            _clientUI.ActivateYesButton();
    }

    public void Wait()
    {
        ChangeState(_waitState);
    }

    public void Pay()
    {
        if (InGroup()) {
            Sit();
            _table.CheckTalk();
        } else {
            if (ClientType == ClientType.Rich)
                MoneyManager.instance.ChangeMoney(Order.Food.MoneyGet * 100);
            else
                MoneyManager.instance.ChangeMoney(Order.Food.MoneyGet);
            Leave();
        }
    }

    public void Leave()
    {
        ClientLeave?.Invoke(this);
        ClientLeave = null;
        IsLeaving = true;
        ChangeState(_walkState);
        _clientUI.ChangeFoodChoiceState(false);
        _clientUI.ChangeSliderState(false);
    }

    public void FoodRejected()
    {
        if (InGroup())
            _table.DecreaseTalk();
        else
            Leave();
    }

    public void Eat()
    {
        if (InGroup()) {
            _table.AddMoney(Order.Food.MoneyGet);
            _table.EndlessWait();
        }
        
        IsEat = true;
        ChangeState(_eatState); 
        ClientEat?.Invoke(this);
    }

    public void SitWithGroup()
    {
        Sit();
        _table.WaitChanged += _clientUI.ChangeFoodChoiceState;
        _table.WaitChanged += _clientUI.ChangeSliderState;
        _table.CheckWait();
    }

    public void Sit()
    {
        ChangeState(_sitState);
    }

    public void Destroy()
    {
        enabled = false;
        _pool.PutObject(this);   
    }

    public void SetSpotTableFood()
    {
        Spot.SetTableFoodSprite(Order.Food, TableIndex);
    }

    public void ResetSpotTableFood()
    {
        Spot.ResetTableFoodSprite(TableIndex);
    }

    public void ChangeSortingGroup(int newValue)
    {
        _sortingGroup.sortingOrder = newValue;
    }

    public float GetWaitTime()
    {
        return WaitMultiplier * UnityEngine.Random.Range(_minWaitTime, _maxWaitTime);
    }

    public bool InGroup()
    {
        return ClientType == ClientType.Double || ClientType == ClientType.Triple ||
            ClientType == ClientType.Quarter;
    }

    public void ChangeShowingTimeEat(bool value)
    {
        IsEatTimeShow = value;
    }
}
