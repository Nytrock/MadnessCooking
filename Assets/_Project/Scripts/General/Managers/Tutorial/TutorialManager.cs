using UnityEngine;

namespace MadnessCooking.General {
    public class TutorialManager : MonoBehaviour, ISaveable {
        [SerializeField, Interface(typeof(ITutorialPart))] private MonoBehaviour[] _tutorialParts;
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
            return ScenesManager.IsGame() && _data.IsWork && _tutorialParts.Length > 0;
        }

        private void StartTutorial() {
            _currentTutorialPartIndex = 0;
            UpdateNowTutorialPart();
        }

        public void NextTutorialPart() {
            if (!_data.IsWork)
                return;

            _currentTutorialPartIndex++;
            if (_currentTutorialPartIndex >= _tutorialParts.Length) {
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
            if (_currentTutorialPartIndex - 1 >= 0) {
                ITutorialPart previousPart = _tutorialParts[_currentTutorialPartIndex - 1] as ITutorialPart;
                previousPart.PartEnded -= NextTutorialPart;
            }

            ITutorialPart currentPart = _tutorialParts[_currentTutorialPartIndex] as ITutorialPart;
            currentPart.PartEnded += NextTutorialPart;
            currentPart.StartTutorialPart();
        }

        public void LoadSave(GameData data) {
            data.General.TutorialManager ??= new();
            _data = data.General.TutorialManager;
        }

        public void ChangeWorkState(bool isWork) {
            _data.ChangeWorkState(isWork);
        }
    }
}
