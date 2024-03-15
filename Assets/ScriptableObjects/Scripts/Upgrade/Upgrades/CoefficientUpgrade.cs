using UnityEngine;

[CreateAssetMenu(menuName = AssetMenuName + nameof(CoefficientUpgrade))]
public class CoefficientUpgrade : GraphUpgrade
{
    [SerializeField] private float _coefficient;

    public float Coefficient => _coefficient;
}
