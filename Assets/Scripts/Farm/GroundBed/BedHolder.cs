using System.Collections;
using UnityEngine;

public class BedHolder : MonoBehaviour
{
    [SerializeField] private BedType _type;
    private Animator _animator;
    private string _name;
    private int _maxCount;

    public BedType Type => _type;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIngredient(Ingredient ingredient)
    {
        _name = ingredient.name;
        _maxCount = ingredient.MaxCount - 1;
        _animator.Play(_name);
        _animator.SetInteger("leftCount", _maxCount);
        StartCoroutine(SetAnimationSpeed(ingredient.TimeGrow));
    }

    public void ResetAnimation()
    {
        if (_animator.GetInteger("leftCount") == 0) {
            _animator.Play(_name, -1, 0);
            _animator.SetInteger("leftCount", _maxCount + 1);
        } else {
            _animator.SetInteger("leftCount", _maxCount);
        }
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
        _animator.SetFloat("growTime", 1 / timeGrow * animationLength);
    }
}
