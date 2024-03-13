using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TechnicHolder : MonoBehaviour
{
    [SerializeField] private Technic _technic;
    [SerializeField] private Transform _UITarget;
    private Animator _animator;

    private bool _isCooking;
    private bool _isRepairing;
    private float _nowStrength;
    private Order _nowOrder;

    private TechnicManager _manager;
    private TechnicCooker _cooker;
    private TechnicRepair _repair;

    public Technic Technic => _technic;
    public bool IsCooking => _isCooking;
    public bool IsRepairing => _isRepairing;
    public float NowStrength => _nowStrength;
    public Transform UITarget => _UITarget;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _cooker = GetComponent<TechnicCooker>();
        _repair = GetComponent<TechnicRepair>();
    }

    private void Start()
    {
        _nowStrength = _technic.Strength;
    }

    public void Activate(TechnicManager technicManager)
    {
        gameObject.SetActive(true);
        _manager = technicManager;
    }

    public void StartCook(Order order)
    {
        _isCooking = true;
        _nowStrength = Mathf.Max(_nowStrength - Random.Range(1f, 2f) / _manager.TechnicStrength, 0);
        _animator.SetBool("isCooking", true);

        _nowOrder = order;
        _cooker.StartWork(order.Food.TimeToCook / _manager.TechnicCookSpeed);
    }

    public void StopCook()
    {
        if (!_isCooking)
            return;

        _isCooking = false;
        _animator.SetBool("isCooking", false);
        _nowOrder.FinishCook();
        _nowOrder = null;
    }

    public void StartRepair()
    {
        _isRepairing = true;
        MoneyManager.instance.ChangeMoney(-_technic.CostRepair);
        _repair.StartWork(_technic.TimeRepair / _manager.TechnicRepairSpeed);
    }

    public void StopRepair()
    {
        _isRepairing = false;
    }
}
