using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class BedHolder : MonoBehaviour
{
    [SerializeField] private BedType _type;
    private Animator _animator;
    private float _speed;

    public BedType Type => _type;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIngredient(Ingredient ingredient)
    {
        _animator.Play(ingredient.name);
        StartCoroutine(SetAnimationSpeed(ingredient.TimeGrow));
    }

    public void SetGrow(bool isActive)
    {
        _animator.enabled = !isActive;
    }

    public void StopAnimation()
    {
        _animator.SetTrigger("disabled");
        _animator.SetFloat("growTime", 1);
    }

    private IEnumerator SetAnimationSpeed(float timeGrow)
    {
        yield return new WaitForEndOfFrame();
        var animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
        _speed = 1 / timeGrow * animationLength;
        _animator.SetFloat("growTime", _speed);
    }
}
