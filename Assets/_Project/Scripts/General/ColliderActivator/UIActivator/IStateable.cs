using System;

public interface IStateable {
    void ChangeState(bool newState);
    event Action<bool> StateChanged;
}
