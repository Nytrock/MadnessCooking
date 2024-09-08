using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FarmShopRobot : MonoBehaviour {
    [SerializeField] private FarmShop _shop;

    [SerializeField] private AnimatedText _noteText;
    [SerializeField] private string _noteStart = "FarmShopRobot.";
    [SerializeField] private UpgradeType[] _acceptableNoteTypes;

    private Animator _animator;
    private string _note;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _shop.ItemSelected += CheckSelectedUpgrade;
        _shop.ItemBought += delegate { ChangeState(FarmShopRobotState.Money); };
    }

    private void CheckSelectedUpgrade(BaseUpgrade upgrade) {
        if (upgrade == null) {
            ChangeState(FarmShopRobotState.Default);
            return;
        }

        foreach (var upgradeType in _acceptableNoteTypes) {
            if (upgrade.Type == upgradeType) {
                _note = $"{_noteStart}{upgradeType}";
                ChangeState(FarmShopRobotState.Note);
                return;
            }
        }

        ChangeState(FarmShopRobotState.Default);
    }

    private void ChangeState(FarmShopRobotState state) {
        _animator.SetBool("isNote", state == FarmShopRobotState.Note);
        if (state == FarmShopRobotState.Money)
            _animator.SetTrigger("isMoney");

        if (state == FarmShopRobotState.Note)
            _noteText.SetText(_note);
    }
}
