using UnityEngine;

public class TutorialActivator : ColliderActivator {
    [SerializeField] private TutorialManager _tutorialManager;

    protected override void Press() {
        _tutorialManager.NextTutorialPart();
    }
}
