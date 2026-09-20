using System;

namespace MadnessCooking.General {
    public interface IStateable {
        void ChangeState(bool newState);
        event Action<bool> StateChanged;
    }
}
