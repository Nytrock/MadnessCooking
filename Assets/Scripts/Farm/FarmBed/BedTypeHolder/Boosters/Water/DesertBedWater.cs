using UnityEngine;

public class DesertBedWater : StandardBedWater
{
    [SerializeField] private float _boostFine;
    private float _originalBoost;

    private void Start()
    {
        _originalBoost = _boostMultiplier;
    }

    public override void StartBoost()
    {
        if (_data.IsBoosting)
            _boostMultiplier = Mathf.Max(0, _boostMultiplier - _boostFine);
        else
            _boostMultiplier = _originalBoost;

        base.StartBoost();
    }
}
