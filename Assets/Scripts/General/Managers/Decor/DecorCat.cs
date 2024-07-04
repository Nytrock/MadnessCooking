using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DecorCat : DecorHolder {
    [SerializeField, Min(0)] private float _fatigueDecreaseCoef;

    private void OnMouseDown() {
        FatigueManager.Instance.ChangeFatigue(-_fatigueDecreaseCoef);
    }
}
