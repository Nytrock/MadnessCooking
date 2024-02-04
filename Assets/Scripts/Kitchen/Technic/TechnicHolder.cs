using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TechnicHolder : MonoBehaviour
{
    [SerializeField] private Technic _technic;
    [SerializeField] private Transform _UITarget;
    private Animator _animator;
    private TechnicHolderUI _UI;

    private float _nowTime;
    private float _cookTime;

    private bool _isFree = true;
    private float _nowStrength;

    public Technic Technic => _technic;
    public bool IsFree => _isFree;
    public float NowStrength => _nowStrength;
    public Transform UITarget => _UITarget;
    public float NowTime => _nowTime;
    public float CookTime => _cookTime;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _isFree = true;
    }

    private void Start()
    {
        _nowStrength = _technic.Strength;
    }

    private void Update()
    {
        if (_isFree)
            return;

        if (_nowTime < _cookTime)
            _nowTime += Time.deltaTime;
        else
            MakeFree();
    }

    public void StartWork(Food food)
    {
        _isFree = false;
        _nowStrength = Mathf.Max(_nowStrength - Random.Range(1f, 2f), 0);
        _animator.SetBool("isCooking", true);

        _nowTime = 0;
        _cookTime = food.TimeToCook;
    }

    public void MakeFree()
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
}
