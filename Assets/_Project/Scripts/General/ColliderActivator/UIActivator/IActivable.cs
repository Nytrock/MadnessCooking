using System;

public interface IActivable {
    void ChangeState(bool newState);
    event Action<bool> StateChanged;
}
