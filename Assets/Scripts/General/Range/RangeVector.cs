using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class RangeVector {
    [SerializeField] private Transform _leftDown;
    [SerializeField] private Transform _rightUp;

    public Vector2 RandomValue => new(
        Random.Range(_leftDown.position.x, _rightUp.position.x),
        Random.Range(_leftDown.position.y, _rightUp.position.y)
    );

    public Vector2 InverseLerp(Vector2 value) {
        float x = Mathf.InverseLerp(_leftDown.position.x, _rightUp.position.x, value.x);
        float y = Mathf.InverseLerp(_leftDown.position.y, _rightUp.position.y, value.y);
        return new(x, y);
    }

    public Vector2 Lerp(Vector2 value) {
        float x = Mathf.Lerp(_leftDown.position.x, _rightUp.position.x, value.x);
        float y = Mathf.Lerp(_leftDown.position.y, _rightUp.position.y, value.y);
        return new(x, y);
    }
}
