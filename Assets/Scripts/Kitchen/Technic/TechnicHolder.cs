using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TechnicHolder : MonoBehaviour
{
    [SerializeField] private Technic _technic;
    [SerializeField] private Transform _UITarget;
    private Animator _animator;
    private TechnicHolderUI _UI;

    private bool _isFree = true;
    private bool _isRepairing;
    private float _nowStrength;

    private TechnicCooker _cooker;
    private TechnicRepair _repair;

    public Technic Technic => _technic;
    public bool IsFree => _isFree;
    public bool IsRepairing => _isRepairing;
    public float NowStrength => _nowStrength;
    public Transform UITarget => _UITarget;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isFree = true;

        _cooker = GetComponent<TechnicCooker>();
        _repair = GetComponent<TechnicRepair>();
    }

    private void Start()
    {
        _nowStrength = _technic.Strength;
    }

    public void StartCook(Food food)
    {
        _isFree = false;
        _nowStrength = Mathf.Max(_nowStrength - Random.Range(1f, 2f), 0);
        _animator.SetBool("isCooking", true);

        _cooker.StartWork(food.TimeToCook);
    }

    public void StopCook()
    {
        _isFree = true;
        _animator.SetBool("isCooking", false);
    }

    void OnMouseDown()
    {
        _UI.OpenTechnic(this);
    }

    public void SetUI(TechnicHolderUI UI)
    {
        _UI = UI;
    }

    public void StartRepair()
    {
        _isRepairing = true;
        MoneyManager.instance.ChangeMoney(-_technic.CostRepair);
        _repair.StartWork(_technic.TimeRepair);
    }

    public void StopRepair()
    {
        _isRepairing = false;
    }
}
