using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SortingGroup), typeof(Animator))]
public class ClientSkin : MonoBehaviour {
    [SerializeField, Min(0)] private int _foregroundSortingLayer;
    [SerializeField, Min(0)] private int _backgroundSortingLayer;

    private Transform _skin;
    private SortingGroup _sortingGroup;
    private Animator _animator;

    private void Awake() {
        _skin = transform;
        _sortingGroup = GetComponent<SortingGroup>();
        _animator = GetComponent<Animator>();
        _sortingGroup.sortingOrder = _foregroundSortingLayer;
    }

    public void StartNewCycle(ClientType clientType) {

    }

    public void RotateSkin(Direction direction) {
        if (direction == Direction.Right)
            _skin.localScale = new Vector2(-1, 1);
        else
            _skin.localScale = Vector2.one;
    }

    public void ChangeSortingLayer() {
        if (_sortingGroup.sortingOrder == _foregroundSortingLayer) {
            _sortingGroup.sortingOrder = _backgroundSortingLayer;
        } else {
            _sortingGroup.sortingOrder = _foregroundSortingLayer;
        }
    }

    public void ChangeWalkState(bool isWalk) {
        _animator.SetBool("isWalk", isWalk);
    }
}