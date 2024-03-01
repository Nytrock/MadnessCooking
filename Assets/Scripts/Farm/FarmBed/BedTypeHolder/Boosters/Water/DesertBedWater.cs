using UnityEngine;

public class DesertBedWater : StandardBedWater
{
    [SerializeField] private float _boostFine;
    private float _originalBoost;

    private void Start()
    {
        _originalBoost = _boostMultiplier;
    }

    public override float StartBoost()
    {
        if (_isBoosting)
            _boostMultiplier = Mathf.Max(0, _boostMultiplier - _boostFine);
        else
            _boostMultiplier = _originalBoost;

        return base.StartBoost();
    }
}
