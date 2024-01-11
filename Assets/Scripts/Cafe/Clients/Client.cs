using UnityEngine;
using UnityEngine.Rendering;

public class Client : MonoBehaviour
{
    #region States Settings
    private ClientState _nowState;
    public ClientState NowState
    {
        get { return _nowState; }
        set { _nowState?.ExitState(); _nowState = value; _nowState.EnterState(this); }
    }
    public ClientWalkState _walkState { get; private set; } = new();
    public ClientWaitState _waitState { get; private set; } = new();
    public ClientEatingState _eatState { get; private set; } = new();
    #endregion

    [SerializeField] private SortingGroup _sortingGroup;
    
    private Transform _enterTarget;
    private Transform _exitTarget;
    private ClientType _clientType;
    private bool _isLeaving = false;

    public Transform EnterTarget => _enterTarget;
    public Transform ExitTarget => _exitTarget;
    public bool IsLeaving => _isLeaving;
    public SortingGroup SortingGroup => _sortingGroup;

    private void Start()
    {
        NowState = _walkState;
        SetCloth();
    }

    private void Update()
    {
        NowState.UpdateState();
    }

    private void SetCloth()
    {

    }

    public void SetTargets(Transform enterTarget, Transform exitTarget)
    {
        _enterTarget = enterTarget;
        _exitTarget = exitTarget;
    }

    public void SetType(ClientType clientType)
    {
        _clientType = clientType;
    }

    public void Leave()
    {
        _isLeaving = true;
        NowState = _walkState;
    }
}
