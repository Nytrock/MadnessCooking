using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ClientData {
    [SerializeField] private ClientType _type;
    [SerializeField] private ClientSkinType _skinType;
    [SerializeField] private ClientState _state;
    [SerializeField] private SerializableVector _position;
    [SerializeField] private float _waitTime;
    [SerializeField] private float _nowTime;
    [SerializeField] private float _waitMultiplier;
    [SerializeField] private bool _isEated;
    [SerializeField] private Order _order;

    public ClientType Type => _type;
    public ClientSkinType SkinType => _skinType;
    public ClientState State => _state;
    public SerializableVector Position => _position;
    public float WaitTime => _waitTime;
    public float NowTime => _nowTime;
    public bool IsEated => _isEated;
    public Order Order => _order;

    public ClientData(Vector3 position, ClientType clientType, float waitMultiplier, Order order) {
        _type = clientType;
        _state = ClientState.Spawn;
        _position = new SerializableVector(position);
        _waitMultiplier = waitMultiplier;
        _order = order;
    }

    public void UpdateTime() {
        _nowTime += InGameTime.Instance.DeltaTime;
    }

    public void SetWaitTime(float minWaitTime, float maxWaitTime) {
        if (_waitTime == 0)
            _waitTime = _waitMultiplier * Random.Range(minWaitTime, maxWaitTime);
    }

    public void ChangeState(ClientState newState) {
        if (_state == ClientState.Eat)
            _isEated = true;
        _state = newState;

        if (newState == ClientState.Eat) {
            _waitTime = _order.Food.TimeToEat * Random.Range(0.9f, 1.2f);
            _nowTime = 0;
        }
    }

    public void SetSkinType(ClientSkinType clientSkinType) {
        _skinType = clientSkinType;
    }

    public void UpdatePosition(Vector3 position) {
        _position = new(position);
    }
}
