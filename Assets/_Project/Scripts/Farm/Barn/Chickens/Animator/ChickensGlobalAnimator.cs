using UnityEngine;

public class ChickensGlobalAnimator : MonoBehaviour {
    [SerializeField] private BarnChickens _chickens;
    [SerializeField] private ChickenAnimator[] _chickensAnimators;

    private void Awake() {
        _chickens.SpeedChanged += UpdateChickensSpeed;
        _chickens.FeedStateChanged += UpdateChickensState;
    }

    private void UpdateChickensState() {
        foreach (var chicken in _chickensAnimators)
            chicken.ChangeState(_chickens.Data.IsFeed);
    }

    private void UpdateChickensSpeed() {
        foreach (var chicken in _chickensAnimators)
            chicken.SetSpeed(_chickens.Data.Speed);
    }
}
