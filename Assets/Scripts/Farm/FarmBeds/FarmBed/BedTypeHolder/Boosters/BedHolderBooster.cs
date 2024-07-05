using System;
using UnityEngine;

public abstract class BedHolderBooster : MonoBehaviour {
    [SerializeField] private SpriteRenderer _boostSprite;
    [SerializeField] protected float _boostMultiplier;
    [SerializeField] protected float _boostLength;
    [SerializeField] protected float _defaultSpeed;

    protected BedHolderBoosterData _data;

    public event Action BoostEnded;

    protected virtual void Awake() {
        ChangeSpriteAlpha(0);
    }

    private void Update() {
        if (!_data.IsBoosting || _data.IsEternal)
            return;

        if (_data.NowTime < _boostLength) {
            _data.UpdateTime();
            ChangeSpriteAlpha(1 - (_data.NowTime / _boostLength));
        } else {
            EndBoost();
        }
    }

    public virtual void SetData(BedHolderBoosterData data) {
        _data = data;
        if (_data.IsEternal)
            ChangeSpriteAlpha(1);
    }

    public virtual void StartBoost() {
        _data.StartBoost(_boostMultiplier);
        ChangeSpriteAlpha(1);
    }

    public virtual void EndBoost() {
        _data.EndBoost(_defaultSpeed);
        ChangeSpriteAlpha(0);
        BoostEnded?.Invoke();
    }

    private void ChangeSpriteAlpha(float alpha) {
        Color color = _boostSprite.color;
        color.a = alpha;
        _boostSprite.color = color;
    }

    public void ChangeEternal() {
        if (_data.IsEternal && !_data.IsBoosting)
            StartBoost();
        else if (_data.IsBoosting)
            EndBoost();
    }
}
