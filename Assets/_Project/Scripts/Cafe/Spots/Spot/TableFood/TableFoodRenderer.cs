using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TableFoodRenderer : MonoBehaviour {
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private string _animationName = "isShow";

    private Animator _animator;
    private Food _nowFood;

    public Food NowFood => _nowFood;

    public event Action<bool> StateChanged;

    private void Awake() {
        _animator = GetComponent<Animator>();
        HideFood();
    }

    public void ShowFood(Food food) {
        _spriteRenderer.sprite = food.MiniSprite;
        _nowFood = food;
        ChangeState(true);
    }

    public void HideFood() {
        _nowFood = null;
        ChangeState(false);
    }

    private void ChangeState(bool newState) {
        _animator.SetBool(_animationName, newState);
        StateChanged?.Invoke(newState);
    }
}
