using System.Collections;
using UnityEngine;

public class BedHolder : MonoBehaviour
{
    [SerializeField] private BedType _type;
    [SerializeField] private GroundBed _groundBed;
    [SerializeField] private BedHolderUI _holderUI;
    private Animator _animator;
    private string _name;
    private int _maxCount;

    public BedType Type => _type;
    public BedHolderUI HolderUI => _holderUI;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeMode(bool newMode)
    {
        gameObject.SetActive(newMode);
    }

    public void SetIngredient(Ingredient ingredient)
    {
        _name = ingredient.name;
        _maxCount = ingredient.MaxCount - 1;
        _animator.Play(_name);
        _animator.SetInteger("leftCount", _maxCount);
        StartCoroutine(SetAnimationSpeed(ingredient.TimeGrow));
    }

    public void ResetAnimation(bool isFull)
    {
        if (_animator.GetInteger("leftCount") == 0 && isFull) {
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
