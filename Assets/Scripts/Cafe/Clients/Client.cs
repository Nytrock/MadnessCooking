using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(ClientUI))]
public class Client : MonoBehaviour
{
    #region States Settings
    private ClientState _nowState;
    public ClientState NowState
    {
        get { return _nowState; }
        set { _nowState?.ExitState(); _nowState = value; _nowState.EnterState(this); }
    }
    private ClientWalkState _walkState = new();
    private ClientWaitState _waitState = new();
    private ClientEatingState _eatState = new();
    #endregion

    [SerializeField] private SortingGroup _sortingGroup;
    [SerializeField] private float _waitTime;

    private ClientUI _clientUI;
    private ClientType _clientType;
    private ClientsPool _pool;
    private CafeSpot _spot;
    private int _spotIndex;

    public Transform EnterTarget { get; private set; }
    public Transform ExitTarget { get; private set; }
    public float WaitMultiplier { get; private set; }
    public bool IsLeaving { get; private set; }
    public Order Order { get; private set; }

    public event Action<Order> OrderActivated;
    public event Action<CafeSpot> ClientLeave;

    private void Awake()
    {
        _clientUI = GetComponent<ClientUI>();
    }

    public void StartNewCycle()
    {
        _waitState = new();
        _eatState = new();

        IsLeaving = false;
        gameObject.SetActive(true);
        NowState = _walkState;
        SetCloth(); 
        _clientUI.StartNewCycle();
    }

    private void Update()
    {
        NowState.UpdateState();
    }

    private void SetCloth()
    {
        // Ставим скин персонажа
    }

    public void SetTargets(Transform enterTarget, Transform exitTarget)
    {
        EnterTarget = enterTarget;
        ExitTarget = exitTarget;
    }

    public void SetType(ClientType clientType)
    {
        _clientType = clientType;
    }

    public void SetOrder(Order order)
    {
        Order = order;
    }

    public void SetWaitingtime(float multiplier)
    {
        WaitMultiplier = multiplier;
    }

    public void SetPool(ClientsPool pool)
    {
        _pool = pool;
    }

    public void SetSpot(CafeSpot spot, int spotIndex = 0)
    {
        _spot = spot;
        _spotIndex = spotIndex;
    }

    public void ActivateOrder()
    {
        OrderActivated?.Invoke(Order);
        _clientUI.SetFood(Order.Food);
    }

    public void CheckOrder()
    {
        if (Order.IsFinished)
            _clientUI.ActivateYesButton();
    }

    public void Wait()
    {
        NowState = _waitState;
    }

    public void PayAndGoAway()
    {
        MoneyManager.Instance.ChangeMoney(Order.Food.MoneyGet);
        _spot.ResetTableFoodSprite(_spotIndex);
        Leave();
    }

    public void Leave()
    {
        ClientLeave?.Invoke(_spot);
        IsLeaving = true;
        NowState = _walkState;
    }

    public void Eat()
    {
        NowState = _eatState;
    }

    public void Destroy()
    {
        _pool.PutObject(this);   
    }

    public void SetSpotTableFood()
    {
        _spot.SetTableFoodSprite(Order.Food, _spotIndex);
    }

    public void ChangeSortingGroup(int newValue)
    {
        _sortingGroup.sortingOrder = newValue;
    }

    public float GetWaitTime()
    {
        return _waitTime * WaitMultiplier * UnityEngine.Random.Range(0.8f, 1.1f);
    }
}
