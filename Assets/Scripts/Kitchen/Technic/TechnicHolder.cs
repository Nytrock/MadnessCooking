using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TechnicHolder : MonoBehaviour
{
    [SerializeField] private Technic _technic;
    [SerializeField] private Animator _animator;
    private bool _isFree = true;

    public Technic Technic => _technic;
    public bool IsFree => _isFree;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartWork(Food food)
    {
        _isFree = false;
    }

    public void MakeFree()
    {
        _isFree = true;
    }

    void OnMouseDown()
    {

    }
}
