using System;
using UnityEngine;

public class BedHolderBooster : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _boostSprite;
    [SerializeField] protected float _boostMultiplier;
    [SerializeField] protected float _boostLength;

    private float _nowTime;
    private float _boostTime;
    private float _boostStep;

    protected bool _isBoosting;
    protected float _standardSpeed = 1;

    public event Action<float> BoostEnded;

    private void Start()
    {
        ChangeSpriteAlpha(0);
    }

    private void Update()
    {
        if (!_isBoosting)
            return;

        if (_nowTime < _boostTime) {
            _nowTime += Time.deltaTime;
            _boostSprite.color -= new Color(0, 0, 0, _boostStep);
        } else {
            EndBoost();
        }
    }

    public virtual float StartBoost()
    {
        _isBoosting = true;

        _boostTime = _boostLength;
        _nowTime = 0;

        ChangeSpriteAlpha(1);
        _boostStep = 1 / _boostTime * Time.deltaTime;

        return _boostMultiplier;
    }

    public virtual void EndBoost()
    {
        _isBoosting = false;
        ChangeSpriteAlpha(0);
        BoostEnded?.Invoke(_standardSpeed);
    }

    private void ChangeSpriteAlpha(float alpha)
    {
        var color = _boostSprite.color; 
        color.a = alpha;
        _boostSprite.color = color;
    }
}
