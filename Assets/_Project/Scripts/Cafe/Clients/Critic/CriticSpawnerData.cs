using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable, JsonObject(MemberSerialization.OptIn)]
public class CriticSpawnerData {
    [SerializeField, JsonProperty] bool _isWaitingCritic;

    public bool IsWaitingCritic => _isWaitingCritic;

    public void ChangeCriticWait(bool newValue) {
        _isWaitingCritic = newValue;
    }
}
