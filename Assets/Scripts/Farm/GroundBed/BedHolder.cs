using UnityEngine;

public class BedHolder : MonoBehaviour
{
    [SerializeField] private BedType _type;
    private Animator _animator;

    public BedType Type => _type;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
