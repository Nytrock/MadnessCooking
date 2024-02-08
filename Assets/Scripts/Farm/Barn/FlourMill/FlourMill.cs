using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FlourMill : HoldDoubleAdd
{
    [SerializeField] private WheatManager _wheatManager;
    [SerializeField] private BarnFridge _barnFridge;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        _wheatManager.WheatChanged += SetWheatNum;
        _barnFridge.FlourChanged += UpdateFlourCount;
        base.Start();
    }

    public override void ChangeWorkMode(bool newValue)
    {
        _animator.SetBool("isHold", newValue);
        base.ChangeWorkMode(newValue);
    }

    private void SetWheatNum(int num)
    {
        _materialCount = num;
        _UI.SetCountText(_materialCount, _readyCount);
    }

    private void UpdateFlourCount(int count)
    {
        if (count > 0)
            return;

        _readyCount = count;
        _UI.SetCountText(_materialCount, count);
    }

    protected override void Add()
    {
        base.Add();
        _wheatManager.SubstractWheat();
        _barnFridge.AddFlour(1);
    }
}
