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
    private ClientSitState _sitState = new();
    #endregion

    [SerializeField] private Transform _skin;
    [SerializeField] private SortingGroup _sortingGroup;
    [SerializeField] private float _waitTime;

    private ClientUI _clientUI;
    private ClientType _clientType;
    private ClientsPool _pool;
    private CafeSpot _spot;
    private ClientGroupHolder _table;

    public Transform EnterTarget { get; private set; }
    public Transform ExitTarget { get; private set; }
    public float WaitMultiplier { get; private set; }
    public bool IsLeaving { get; private set; }
    public Order Order { get; private set; }
    public int SpotIndex { get; private set; }

    public event Action<Client> OrderActivated;
    public event Action<Client> ClientLeave;

    private void Awake()
    {
        _clientUI = GetComponent<ClientUI>();
        enabled = false;
    }

    public void StartNewCycle()
    {
        enabled = true;
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

    public void RotateSkin(bool isRight)
    {
        if (isRight)
            _skin.localScale = Vector2.one;
        else
            _skin.localScale = new Vector2(-1, 1);
    }

    public void RotateSkin()
    {
        _skin.localScale = new Vector2(_spot.GetTableRotation(SpotIndex), 1);
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

    public void SetSpot(CafeSpot spot, int spotIndex)
    {
        _spot = spot;
        if (InGroup())
            _table = _spot.GetComponent<ClientGroupHolder>();
        SpotIndex = spotIndex;
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
        NowState = _waitState;
    }

    public void Pay()
    {
        _spot.ResetTableFoodSprite(SpotIndex);
        if (InGroup()) {
            Sit();
            _table.CheckTalk();
        } else {
            MoneyManager.instance.ChangeMoney(Order.Food.MoneyGet);
            Leave();
        }
    }

    public void Leave()
    {
        ClientLeave?.Invoke(this);
        ClientLeave = null;
        IsLeaving = true;
        NowState = _walkState;
        _clientUI.SetUIVisible(false);
    }

    public void FoodRejected()
    {
        if (InGroup()) {
            Sit();
            _table.DecreaseTalk();
            _table.CheckTalk();
        } else {
            Leave();
        }
    }

    public void Eat()
    {
        if (InGroup()) {
            _table.AddMoney(Order.Food.MoneyGet);
            _table.CheckWait();
        }
        _clientUI.SetUIVisible(false);
        NowState = _eatState;
    }

    public void Sit()
    {
        NowState = _sitState;
    }

    public void Destroy()
    {
        enabled = false;
        _pool.PutObject(this);   
    }

    public void SetSpotTableFood()
    {
        _spot.SetTableFoodSprite(Order.Food, SpotIndex);
    }

    public void ChangeSortingGroup(int newValue)
    {
        _sortingGroup.sortingOrder = newValue;
    }

    public float GetWaitTime()
    {
        return _waitTime * WaitMultiplier * UnityEngine.Random.Range(0.8f, 1.1f);
    }

    public bool InGroup()
    {
        return _clientType == ClientType.Double || _clientType == ClientType.Triple ||
            _clientType == ClientType.Quarter;
    }

    public void DisableWait()
    {
        if (_nowState != _sitState) {
            Sit();
            _clientUI.SetUIVisible(true);
        }
    }
}
