using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MadnessCooking.General {
    public class GameSaveManager : SaveManager<GameData> {
        [SerializeField, Interface(typeof(ISaveable))] private MonoBehaviour[] _saveables;
        protected override string FileName => "save/save1";

        public event Action BeforeLateStart;
        public event Action AfterLateStart;

        protected override void UpdateData() {
            IEnumerable<ISaveable> saveables = _saveables.Cast<ISaveable>();
            foreach (var saveable in saveables)
                saveable.LoadSave(_data);

            BeforeLateStart?.Invoke();
            foreach (var saveable in saveables)
                saveable.LateStart();
            AfterLateStart?.Invoke();
        }

        [ContextMenu("Save")]
        private void SaveByEditor() {
            if (!Application.isPlaying)
                return;

            Save();
        }

        [ContextMenu("Delete")]
        private void DeleteByEditor() {
            _dataService = new(FileName);
            _dataService.Delete();
        }
    }
}
