using System;
using UnityEngine;

[RequireComponent(typeof(ClientUI))]
public class Client : MonoBehaviour {
    private ClientBaseState _nowState;
    private readonly ClientWalkState _walkState = new();
    private readonly ClientEatState _eatState = new();
    private readonly ClientSitState _sitState = new();

    [SerializeField] private ClientSkin _skin;
    [SerializeField] private ClientGender _gender;
    [SerializeField] private RangeFloat _waitTime;
    [SerializeField] private int _richClientMoneyMultiplier = 2;

    private ClientsHolder _table;
    private CafeSpot _spot;
    private CafeSeat _seat;

    [field: SerializeField] public ClientData Data { get; private set; }
    public ClientsSpawner Spawner { get; private set; }
    public ClientUI ClientUI { get; private set; }
    [field: SerializeField] public int SpotIndex { get; private set; }
    [field: SerializeField] public int SeatIndex { get; private set; }

    public ClientGender Gender => _gender;

    public event Action<Client> OrderActivated;
    public event Action<Client> ClientLeave;
    public event Action<Client> ClientRejected;
    public event Action<Client> ClientEat;
    public event Action ClientSetup;

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
            case ClientState.Wait:
            case ClientState.Sit:
                clientState = _sitState;
                break;
        }

        _nowState?.ExitState(this);
        _nowState = clientState;
        _nowState.EnterState(this);
    }

    private void Update() {
        _nowState.UpdateState(this);
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
        Spawner = settings.Spawner;

        SpotIndex = settings.SpotIndex;
        SeatIndex = settings.SeatIndex;

        Data = settings.Data;
        Data.SetGender(_gender);
        Data.SetWaitTime(_waitTime.RandomValue);

        transform.position = Data.Position.GetVector();
        _skin.StartNewCycle(Data);

        ClientUI.Setup(Data, ActivateOrder, Spawner.TutorialManager);
        ClientUI.StartNewCycle();

        if (Data.State == ClientState.Leave) {
            ChangeState();
            ChangeEnable(true);
            return;
        }

        _spot = Spawner.GetSpot(SpotIndex);
        _seat = _spot.GetSeat(SeatIndex);
        ChangeState();

        if (Data.State != ClientState.Spawn)
            TakeSeat();

        if (!_spot.TryGetComponent(out _table))
            throw new ArgumentNullException("Spot doesn't have the required class ClientGroupHolder");
        _table.WaitStarted += Sit;

        ClientSetup?.Invoke();
    }

    public void ActivateOrder() {
        OrderActivated?.Invoke(this);
        ClientUI.SetFood(Data.Order.Food);
        Data.Order.Activate();
    }

    public void CheckOrder() {
        if (Data.Order.IsFinished)
            ClientUI.ActivateYesButton();
    }

    public void Wait() {
        WaitOthers();
        _table.CheckWait();
    }

    public void EndEat() {
        WaitOthers();
        _table.CheckTalk();
    }

    public void CheckCafe(bool isOpened) {
        if (isOpened)
            return;

        Leave();
    }

    public void Leave() {
        ClientLeave?.Invoke(this);
        if (!Data.IsEated)
            ClientRejected?.Invoke(this);
        Data.ChangeState(ClientState.Leave);

        ChangeState();
        ClientUI.ChangeSliderState(false);
        ClientUI.ChangeFoodChoiceState(false);
        _seat.ChangeSeatState(false);
    }

    public void FoodRejected() {
        WaitOthers();
        _table.DecreaseTalk();
        ClientRejected?.Invoke(this);
    }

    public void Eat() {
        int payingMoney = Data.Order.Food.MoneyGet;
        if (Data.Type == ClientType.Rich)
            payingMoney *= _richClientMoneyMultiplier;
        _table.AddMoney(payingMoney);

        _table.StartEndlessWait();
        Data.ChangeState(ClientState.Eat);
        ChangeState();
        ClientEat?.Invoke(this);
    }

    public void Sit() {
        Data.ChangeState(ClientState.Sit);
        ChangeState();
    }

    public void Destroy() {
        Spawner.PutClient(this);
    }

    public void SetSpotTableFood() {
        _spot.SetTableFoodSprite(Data.Order.Food, SeatIndex);
    }

    public void ResetSpotTableFood() {
        _spot.ResetTableFoodSprite(SeatIndex);
    }

    private void WaitOthers() {
        Data.ChangeState(ClientState.Wait);
        ChangeState();
    }

    public void ChangeEnable(bool value) {
        enabled = value;
        _skin.ChangeEnable(value);
    }

    public void ResetState() {
        _nowState = null;
    }
}
