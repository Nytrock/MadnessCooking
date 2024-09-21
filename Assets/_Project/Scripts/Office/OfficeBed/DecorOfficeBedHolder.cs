using UnityEngine;

public class DecorOfficeBedHolder : DecorObjectChanger {
    [SerializeField] private FatigueManager _fatigueManager;
    [SerializeField] private float _bedSleepCoef;

    public override void ChangeState(bool newValue) {
        base.ChangeState(newValue);
        if (newValue)
            _fatigueManager.MultiplySleepBonus(_bedSleepCoef);
    }
}
