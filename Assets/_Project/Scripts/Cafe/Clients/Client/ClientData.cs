using Newtonsoft.Json;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class ClientData {
    [SerializeField, JsonProperty] private ClientType _type;
    [SerializeField, JsonProperty] private ClientSkinType _skinType;
    [SerializeField, JsonProperty] private ClientState _state;
    [SerializeField, JsonProperty] private ClientGender _gender;
    [SerializeField, JsonProperty] private JsonVector _position;
    [SerializeField, JsonProperty] private float _waitTime;
    [SerializeField, JsonProperty] private float _nowTime;
    [SerializeField, JsonProperty] private float _waitMultiplier;
    [SerializeField, JsonProperty] private bool _isServiced;
    [SerializeField, JsonProperty] private Order _order;
    [SerializeField] private float _waitCoef;

    public ClientType Type => _type;
    public ClientSkinType SkinType => _skinType;
    public ClientState State => _state;
    public ClientGender Gender => _gender;
    public JsonVector Position => _position;
    public float WaitTime => _waitTime;
    public float NowTime => _nowTime;
    public float WaitCoef => _waitCoef;
    public bool IsServiced => _isServiced;
    public Order Order => _order;

    public ClientData(Vector3 position, ClientType clientType, float waitMultiplier, Order order) {
        _type = clientType;
        _state = ClientState.Spawn;
        _position = new JsonVector(position);
        _waitMultiplier = waitMultiplier;
        _order = order;
    }

    public void UpdateTime() {
        _nowTime += InGameTime.Instance.NormalizedDeltaTime;
    }

    public void SetWaitTime(float waitTime) {
        if (_waitTime == 0)
            _waitTime = _waitMultiplier * waitTime;
    }

    public void ChangeState(ClientState newState) {
        if (_state == ClientState.Eat)
            Service();
        _state = newState;

        if (newState == ClientState.Eat) {
            _waitCoef = _nowTime / _waitTime;
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

    public void SetGender(ClientGender gender) {
        _gender = gender;
    }

    public void Service() {
        _isServiced = true;
    }
}
