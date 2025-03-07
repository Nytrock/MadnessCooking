using AYellowpaper;
using UnityEngine;

public class TutorialManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField] private InterfaceReference<ITutorialPart>[] _parts;
    [SerializeField] private GameSaveManager _saveManager;

    private int _currentTutorialPartIndex;
    private TutorialManagerData _data;

    public bool IsWork => _data.IsWork;

    public void LateStart() {
        if (!IsTutorial())
            return;

        StartTutorial();
    }

    private bool IsTutorial() {
        return SceneManager.IsGame() && _data.IsWork && _parts.Length > 0;
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
        if (_currentTutorialPartIndex - 1 >= 0)
            _parts[_currentTutorialPartIndex - 1].Value.PartEnded -= NextTutorialPart;

        ITutorialPart currentPart = _parts[_currentTutorialPartIndex].Value;
        currentPart.PartEnded += NextTutorialPart;
        currentPart.StartTutorialPart();
    }

    public void Bind(GeneralData data) {
        data.TutorialManager ??= new();
        _data = data.TutorialManager;
    }

    public void ChangeWorkState(bool isWork) {
        _data.ChangeWorkState(isWork);
    }
}
