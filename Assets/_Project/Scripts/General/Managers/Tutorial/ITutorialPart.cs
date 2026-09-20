using System;

namespace MadnessCooking.General {
    public interface ITutorialPart {
        event Action PartEnded;
        void StartTutorialPart();
    }
}
