using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TechnicHolder : MonoBehaviour
{
    [SerializeField] private Technic _technic;
    [SerializeField] private Transform _UITarget;
    private Animator _animator;

    private KitchenData _data;
    private Order _nowOrder;

    private TechnicCooker _cooker;
    private TechnicRepair _repair;

    public SerializableTechnic TechnicData { get; private set; }

    public Technic Technic => _technic;
    public Transform UITarget => _UITarget;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _cooker = GetComponent<TechnicCooker>();
        _repair = GetComponent<TechnicRepair>();
    }

    public void ChangeState(bool newState)
    {
        gameObject.SetActive(newState);
    }

    public void StartCook(Order order)
    {
        TechnicData.IsCooking = true;
        TechnicData.NowStrength = Mathf.Max(TechnicData.NowStrength - Random.Range(1f, 2f) / _data.TechnicStrength, 0);
        _animator.SetBool("isCooking", true);

        _nowOrder = order;
        _cooker.StartWork(order.Food.TimeToCook / _data.TechnicCookSpeed);
    }

    public void StopCook()
    {
        if (!TechnicData.IsCooking)
            return;

        TechnicData.IsCooking = false;
        _animator.SetBool("isCooking", false);
        _nowOrder.FinishCook();
        _nowOrder = null;
    }

    public void StartRepair()
    {
        TechnicData.IsRepairing = true;
        _repair.StartWork(_technic.TimeRepair / _data.TechnicRepairSpeed);
    }

    public void StopRepair()
    {
        TechnicData.IsRepairing = false;
    }

    public void Bind(KitchenData data, int index, bool isFileEmpty)
    {
        _data = data;

        if (isFileEmpty) {
            _data.AllTechnic[index] = new() {
                NowStrength = _technic.Strength
            };
        }

        TechnicData = _data.AllTechnic[index];
        if (TechnicData.IsRepairing)
            StartRepair();
    }

    public bool Accessible()
    {
        return !TechnicData.IsCooking && TechnicData.NowStrength != 0 && !TechnicData.IsRepairing;
    }

    public bool Repairable()
    {
        return TechnicData.NowStrength != _technic.Strength && 
            MoneyManager.instance.MoneyCount >= _technic.CostRepair;
    }
}
