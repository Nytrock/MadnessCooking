using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class DecorCat : DecorHolder
{
    [SerializeField] private float _fatigueDecreaseCoef;

    private void OnMouseDown()
    {
        FatigueManager.instance.ChangeFatigue(-_fatigueDecreaseCoef);
    }
}
