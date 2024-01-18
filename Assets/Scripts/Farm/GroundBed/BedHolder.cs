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

    public void SetIngredient(Ingredient ingredient)
    {
        _animator.Play(ingredient.name);
        _animator.SetBool("isFull", false);
        _animator.SetFloat("growTime", 1 / (float)ingredient.TimeGrow);
    }

    public void SetGrow(bool newValue)
    {
        _animator.SetBool("isFull", newValue);
    }

    public void StopAnimation()
    {
        _animator.SetTrigger("disabled");
    }
}
