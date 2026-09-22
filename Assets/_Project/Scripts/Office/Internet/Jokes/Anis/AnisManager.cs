using MadnessCooking.General;
using UnityEngine;

namespace MadnessCooking.Office {
    public class AnisManager : MonoBehaviour, ISaveable {
        [SerializeField] private SpriteChanger[] _sprites;
        private JokesData _data;

        public bool IsAnis => _data.IsAnis;

        public void LateStart() {
            UpdateAnisState();
        }

        public void LoadSave(GameData data) {
            data.Office.InternetJokesData ??= new();
            _data = data.Office.InternetJokesData;
        }

        public void ChangeAnisState(bool isAnis) {
            _data.ChangeAnisState(isAnis);
            UpdateAnisState();
        }

        public void UpdateAnisState() {
            foreach (var sprite in _sprites)
                sprite.ChangeState(_data.IsAnis);
        }

        [ContextMenu("SetAnis")]
        public void SetAnisState() {
            foreach (var sprite in _sprites)
                sprite.ChangeState(true);
        }

        [ContextMenu("SetDefault")]
        public void SetDefaultState() {
            foreach (var sprite in _sprites)
                sprite.ChangeState(false);
        }
    }
}
