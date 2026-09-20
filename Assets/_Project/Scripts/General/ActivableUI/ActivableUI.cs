using System;
using UnityEngine;

public abstract class ActivableUI : MonoBehaviour {
    public event Action<bool> StateChanged;

    public virtual void ChangeState(bool newState) {
        StateChanged?.Invoke(newState);
    }
}
