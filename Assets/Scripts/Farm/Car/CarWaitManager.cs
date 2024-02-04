using System;
using UnityEngine;

public class CarWaitManager : MonoBehaviour
{
    [SerializeField] private Car _car;
    [SerializeField] private KitchenStorage _kitchenStorage;
    [SerializeField] private float _waitTime;
    private IngredientCountList _ingredientsSended;
    private float _nowTime;
    private bool _isWait;
    private bool _isSended;

    public event Action<float> WaitStarted;

    public void StartWait()
    {
        _ingredientsSended = _car.GetList();
        _nowTime = 0;
        _isWait = true;
        _car.Leave();
        WaitStarted?.Invoke(_waitTime);
    }

    private void Update()
    {
        if (!_isWait)
            return;

        if (_nowTime < _waitTime) {
            _nowTime += Time.deltaTime;
        } else {
            if (_isSended) {
                _isWait = true;
                _car.Return();
            } else {
                _kitchenStorage.AddIngrediens(_ingredientsSended);
                _nowTime = 0;
                _isSended = true;
            }
        }
    }
}
