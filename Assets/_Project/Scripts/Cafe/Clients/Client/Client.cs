using System;
using UnityEngine;

[RequireComponent(typeof(ClientUI))]
public class Client : MonoBehaviour {
    private ClientBaseState _nowState;
    private readonly ClientWalkState _walkState = new();
    private readonly ClientEatState _eatState = new();
    private readonly ClientWaitState _waitState = new();

    [SerializeField] private ClientSkin _skin;
    [SerializeField] private ClientGender _gender;
    [SerializeField] private int _richClientMoneyMultiplier = 2;

    private ClientsSpawner _spawner;
    private ClientsHolder _holder;
    private CafeSeat _seat;

    public ClientData Data { get; private set; }
    public ClientUI ClientUI { get; private set; }

    public ClientGender Gender => _gender;
    public ClientsHolder Holder => _holder;
    public CafeSeat Seat => _seat;

    public event Action<Client> OrderActivated;
    public event Action<Client> ClientLeave;
    public event Action<Client> ClientRejected;
    public event Action<Client> ClientEat;
    public event Action SetupEnded;

    private void Awake() {
        ClientUI = GetComponent<ClientUI>();
    }

    public void StartNewCycle() {
        _skin.StartNewCycle(Data);
        ClientUI.StartNewCycle();
    }

    private void ChangeState() {
        ClientBaseState clientState = null;
        switch (Data.State) {
            case ClientState.Spawn:
            case ClientState.Leave:
                clientState = _walkState;
                break;
            case ClientState.Eat:
                clientState = _eatState;
                break;
            case ClientState.WaitOrder:
            case ClientState.WaitOthers:
                clientState = _waitState;
                break;
        }

        _nowState?.ExitState(this);
        _nowState = clientState;
        _nowState.EnterState(this);
    }

    private void Update() {
        _nowState?.UpdateState(this);
    }

    public void StartWalk(Direction direction) {
        _skin.ChangeSortingLayer(true);
        _skin.RotateSkin(direction);

        _skin.ChangeWalkState(true);
    }

    private void RotateSkin() {
        _skin.RotateSkin(_seat.SeatDirection);
    }

    public void TakeSeat() {
        RotateSkin();
        _skin.ChangeSortingLayer(false);
        _skin.ChangeWalkState(false);
        _seat.ChangeSeatState(true);
    }

    public void Setup(ClientSettings settings) {
        Data = settings.Data;
        Data.SetGender(_gender);

        transform.position = Data.Position;
        _skin.StartNewCycle(Data);
        ClientUI.StartNewCycle();

        if (Data.State == ClientState.Leave) {
            ChangeState();
            ChangeEnable(true);
            return;
        }

        _seat = settings.Seat;
        _holder = settings.Holder;
        ChangeState();

        if (Data.State != ClientState.Spawn)
            TakeSeat();
    }

    public void EndSetup() {
        SetupEnded?.Invoke();
    }

    public void ActivateOrder() {
        OrderActivated?.Invoke(this);
        ClientUI.SetFood(Data.Order.Food);
        Data.Order.Activate();
    }

    public void FinishOrder() {
        ClientUI.FinishOrder();
    }

    public void SitAndWait() {
        WaitOthers();
        _holder.CheckWait();
    }

    public void EndEat() {
        WaitOthers();
        _holder.CheckVisitEnded();
    }

    public void Leave() {
        ClientLeave?.Invoke(this);
        Data.ChangeState(ClientState.Leave);

        ChangeState();
        _seat.ChangeSeatState(false);
    }

    public void FoodRejected() {
        WaitOthers();
        Data.Service();
        _holder.FoodRejected();
        ClientRejected?.Invoke(this);
    }

    public void CheckIsServiced() {
        if (!Data.IsServiced)
            ClientRejected?.Invoke(this);
    }

    public void Eat() {
        Data.ChangeState(ClientState.Eat);
        ChangeState();

        int payingMoney = Data.Order.Food.MoneyGet;
        if (Data.Type == ClientType.Rich)
            payingMoney *= _richClientMoneyMultiplier;
        _holder.ClientEat(payingMoney);
        ClientEat?.Invoke(this);
    }

    public void WaitOrder() {
        Data.ChangeState(ClientState.WaitOrder);
        ChangeState();
    }

    public void Destroy() {
        ResetState();
        _spawner.PutClient(this);
    }

    public void SetSpotTableFood() {
        _seat.SetTableFoodSprite(Data.Order.Food);
    }

    public void StopEat() {
        ClientUI.ChangeEatSliderState(false);
        _seat.ResetTableFoodSprite();
    }

    private void WaitOthers() {
        Data.ChangeState(ClientState.WaitOthers);
        ChangeState();
    }

    public void ChangeEnable(bool value) {
        enabled = value;
        _skin.ChangeEnable(value);
    }

    public void ResetState() {
        _nowState = null;
        _seat = null;

        OrderActivated = null;
        ClientLeave = null;
        ClientRejected = null;
        ClientEat = null;
    }

    public void SetupOnCreate(ClientsSpawner spawner, TutorialManager tutorialManager,
        UIActivatorsManager UIManager, CafeUpgradeData upgradeData) {
        _spawner = spawner;
        ClientUI.SetupOnCreate(this, tutorialManager, UIManager, upgradeData);
        _walkState.SetupSpawner(spawner);
    }
}
