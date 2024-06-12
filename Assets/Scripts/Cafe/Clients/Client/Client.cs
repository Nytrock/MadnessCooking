using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ClientUI))]
public class Client : MonoBehaviour {
    private ClientBaseState _nowState;
    private readonly ClientWalkState _walkState = new();
    private readonly ClientEatState _eatState = new();
    private readonly ClientSitState _sitState = new();

    [SerializeField] private ClientSkin _skin;
    [SerializeField, Min(0)] private float _minWaitTime;
    [SerializeField, Min(0)] private float _maxWaitTime;
    private ClientsHolder _table;

    public ClientData ClientData { get; private set; }
    public ClientsSpawner Spawner { get; private set; }
    public ClientUI ClientUI { get; private set; }
    public int SpotIndex { get; private set; }
    public int TableIndex { get; private set; }

    public event Action<Client> OrderActivated;
    public event Action<Client> ClientLeave;
    public event Action<Client> ClientRejected;
    public event Action<Client> ClientEat;

    private void Awake() {
        ClientUI = GetComponent<ClientUI>();
    }

    public void StartNewCycle() {
        _skin.StartNewCycle(ClientData);
        ClientUI.StartNewCycle(ActivateOrder);
    }

    private void ChangeState() {
        ClientBaseState clientState = null;
        switch (ClientData.State) {
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
        MoveClient(true);
    }

    private void RotateSkin() {
        CafeSpot spot = Spawner.GetSpot(SpotIndex);
        _skin.RotateSkin(spot.GetSeatRotation(TableIndex));
    }

    public void TakeSeat() {
        RotateSkin();
        _skin.ChangeSortingLayer(false);
        _skin.ChangeWalkState(false);
        MoveClient(false);
    }

    private void MoveClient(bool isWalk) {
        float posY;
        if (isWalk)
            posY = Spawner.SpawnPoint.y;
        else
            posY = Spawner.GetSpot(SpotIndex).GetTarget(TableIndex).y;
        transform.position = new Vector2(transform.position.x, posY);
    }

    public void Setup(ClientSettings settings) {
        Spawner = settings.Spawner;

        TableIndex = settings.TableIndex;
        SpotIndex = settings.SpotIndex;

        ClientData = settings.Data;
        if (ClientData.WaitTime == 0) {
            ClientData.WaitTime = ClientData.WaitMultiplier * Random.Range(_minWaitTime, _maxWaitTime);
        }

        transform.position = ClientData.Position.GetVector();
        ClientUI.Setup(ClientData);
        ChangeState();

        if (ClientData.State != ClientState.Spawn && ClientData.State != ClientState.Leave)
            TakeSeat();

        if (ClientData.State == ClientState.Leave)
            return;

        CafeSpot spot = Spawner.GetSpot(SpotIndex);
        if (!spot.TryGetComponent(out _table))
            throw new ArgumentNullException("Spot doesn't have the required class ClientGroupHolder");
        _table.WaitStarted += Sit;
    }

    public void ActivateOrder() {
        OrderActivated?.Invoke(this);
        ClientUI.SetFood(ClientData.Order.Food);
        ClientData.Order.Activate();
    }

    public void CheckOrder() {
        if (ClientData.Order.IsFinished)
            ClientUI.ActivateYesButton();
    }

    public void Wait() {
        WaitOthers();
        _table.CheckWait();
    }

    public void Pay() {
        WaitOthers();
        _table.CheckTalk();
    }

    public void Leave() {
        ClientLeave?.Invoke(this);
        ClientData.State = ClientState.Leave;

        ChangeState();
        ClientUI.ChangeSliderState(false);
        ClientUI.ChangeFoodChoiceState(false);
    }

    public void FoodRejected() {
        WaitOthers();
        _table.DecreaseTalk();
        ClientRejected?.Invoke(this);
    }

    public virtual void Eat() {
        int payingMoney = ClientData.Order.Food.MoneyGet;
        if (ClientData.Type == ClientType.Rich)
            payingMoney *= 100;
        _table.AddMoney(payingMoney);

        _table.EndlessWait();
        ClientData.WaitTime = ClientData.Order.Food.TimeToEat * Random.Range(0.9f, 1.2f);
        ClientData.NowTime = 0;
        ClientData.State = ClientState.Eat;
        ChangeState();
        ClientEat?.Invoke(this);
    }

    public void Sit() {
        ClientData.State = ClientState.Sit;
        ChangeState();
    }

    public void Destroy() {
        Spawner.PutClient(this);
    }

    public void SetSpotTableFood() {
        CafeSpot spot = Spawner.GetSpot(SpotIndex);
        spot.SetTableFoodSprite(ClientData.Order.Food, TableIndex);
    }

    public void ResetSpotTableFood() {
        CafeSpot spot = Spawner.GetSpot(SpotIndex);
        spot.ResetTableFoodSprite(TableIndex);
    }

    private void InvokeRejected() {

    }

    private void WaitOthers() {
        ClientData.State = ClientState.Wait;
        ChangeState();
    }
}
