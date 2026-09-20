using UnityEngine;
using MadnessCooking.General;

namespace MadnessCooking.Office {
    public class AnisManager : MonoBehaviour, IBindable<OfficeData> {
        [SerializeField] private SpriteChanger[] _sprites;
        private JokesData _data;

        public bool IsAnis => _data.IsAnis;

        public void LateStart() {
            UpdateAnisState();
        }

        public void Bind(OfficeData data) {
            data.InternetJokesData ??= new();
            _data = data.InternetJokesData;
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
