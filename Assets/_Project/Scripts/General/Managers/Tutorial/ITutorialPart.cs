using System;

public interface ITutorialPart {
    event Action PartEnded;
    void StartTutorialPart();
}
