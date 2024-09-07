using UnityEngine;

public class TutorialManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField] private TutorialPart[] _parts;
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private ClueManager _clueManager;
    [SerializeField] private GameSaveManager _saveManager;
    [SerializeField] private BaseShop[] _shops;

    private int _currentTutorialPartIndex;
    private TutorialManagerData _data;

    public bool IsWork => _data.IsWork;

    private void Awake() {
        if (SceneUtility.IsMenu())
            return;

        _dialogueManager.DialogueEnded += NextTutorialPart;
        _clueManager.ClueHided += NextTutorialPart;
    }

    private void LateStart() {
        if (SceneUtility.IsGame() && _data.IsWork && _parts.Length > 0) {
            StartTutorial();
            return;
        }

        foreach (var shop in _shops)
            shop.BuyTutorialItems();
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
        TutorialPart tutorialPart = _parts[_currentTutorialPartIndex];
        if ((tutorialPart as DialogueChoicer) != null) {
            _dialogueManager.StartDialogue(tutorialPart as DialogueChoicer);
        } else if ((tutorialPart as ClueTemplate) != null) {
            _clueManager.ShowClue(tutorialPart as ClueTemplate);
        } else if ((tutorialPart as TutotialClientSpawn) != null) {
            TutotialClientSpawn spawner = tutorialPart as TutotialClientSpawn;
            spawner.SpawnClient();
        }
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
