using AYellowpaper;
using UnityEngine;

public class TutorialManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField] private InterfaceReference<ITutorialPart>[] _parts;
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private ClueManager _clueManager;
    [SerializeField] private GameSaveManager _saveManager;

    [Header("Tutorial items")]
    [SerializeField] private BaseShop[] _shops;
    [SerializeField] private BuyableItem[] _items;

    private int _currentTutorialPartIndex;
    private TutorialManagerData _data;

    public bool IsWork => _data.IsWork;

    private void Awake() {
        if (SceneUtility.IsMenu())
            return;

        _dialogueManager.DialogueEnded += NextTutorialPart;
        _clueManager.ClueHided += NextTutorialPart;
        _saveManager.LoadEnded += BuyTutorialItems;
    }

    private void BuyTutorialItems() {
        _saveManager.LoadEnded -= BuyTutorialItems;
        if (!IsTutorial())
            return;

        foreach (var item in _items)
            foreach (var shop in _shops)
                shop.TryToBuyItem(item);
    }

    private void LateStart() {
        if (!IsTutorial())
            return;

        StartTutorial();
    }

    private bool IsTutorial() {
        return SceneUtility.IsGame() && _data.IsWork && _parts.Length > 0;
    }

    private void StartTutorial() {
        _currentTutorialPartIndex = 0;
        UpdateNowTutorialPart();
    }

    public void NextTutorialPart() {
        if (!_data.IsWork)
            return;

        _currentTutorialPartIndex++;
        if (_currentTutorialPartIndex >= _parts.Length) {
            EndTutorial();
            return;
        }

        UpdateNowTutorialPart();
    }

    private void EndTutorial() {
        _data.ChangeWorkState(false);
        _saveManager.Save();
    }

    private void UpdateNowTutorialPart() {
        if (_currentTutorialPartIndex - 1 > 0)
            _parts[_currentTutorialPartIndex - 1].Value.PartEnded -= NextTutorialPart;

        ITutorialPart currentPart = _parts[_currentTutorialPartIndex].Value;
        currentPart.StartTutorialPart();
        currentPart.PartEnded += NextTutorialPart;
    }

    public void Bind(GeneralData data) {
        data.TutorialManager ??= new();
        _data = data.TutorialManager;
        LateStart();
    }

    public void ChangeWorkState(bool isWork) {
        _data.ChangeWorkState(isWork);
    }
}
