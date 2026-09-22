using MadnessCooking.General;
using System;
using UnityEngine;

namespace MadnessCooking.Cafe {
    public class PopularityManager : MonoBehaviour, ISaveable {
        [SerializeField] private PopularityLevel[] _levels;
        [SerializeField] private PopularityLevel _defaultLevel;

        private PopularityManagerData _data;
        private PopularityLevel NowLevel => _levels[_data.Level];

        public bool IsMaxLevel => _data.Level == _levels.Length - 1;

        public event Action<PopularityLevel> LevelChanged;
        public event Action<int> XpChanged;


        [ContextMenu(nameof(CheckLevels))]
        private void CheckLevels() {
            bool isError = false;
            foreach (var level in _levels) {
                float sumChance = level.SingleChance + level.DoubleChance +
                    level.TripleChance + level.QuarterChance;
                if (Mathf.RoundToInt(sumChance) != 100) {
                    isError = true;
                    Debug.LogError($"Popularity level number {level.Number} has total chance {sumChance}, must be 100");
                }
            }

            if (!isError)
                Debug.Log("All popularity levels are OK");
        }

        private void Awake() {
            foreach (var level in _levels)
                level.Initialize();
        }

        public void LateStart() {
            LevelChanged?.Invoke(NowLevel);
            XpChanged?.Invoke(_data.Xp);
        }

        public void AddXp(float xp) {
            _data.AddXp((int)xp);

            if (_data.Xp >= NowLevel.NeedXp && !IsMaxLevel) {
                while (_data.Xp >= NowLevel.NeedXp && !IsMaxLevel) {
                    if ((_data.Level + 1) % 5 == 0) {
                        _data.RemoveXp(_data.Xp - NowLevel.NeedXp);
                        break;
                    }

                    _data.RemoveXp(NowLevel.NeedXp);
                    NextLevel();
                }
            }
            XpChanged?.Invoke(_data.Xp);
        }

        public void RemoveXp(float xp) {
            _data.RemoveXp((int)xp);
            if (_data.Xp < 0 && _data.Level > 0) {
                while (_data.Xp < 0 && _data.Level > 0) {
                    _data.AddXp(_levels[_data.Level - 1].NeedXp);
                    PreviousLevel();
                }
            }

            if (_data.Xp < 0)
                _data.RemoveXp(_data.Xp);

            XpChanged?.Invoke(_data.Xp);
        }

        public void NextLevel() {
            _data.NextLevel();
            LevelChanged?.Invoke(NowLevel);
        }

        public void PreviousLevel() {
            _data.PreviousLevel();
            LevelChanged?.Invoke(NowLevel);
            if (_data.Xp >= NowLevel.NeedXp)
                _data.RemoveXp(_data.Xp - NowLevel.NeedXp + 1);
        }

        public void LoadSave(GameData data) {
            data.Cafe.PopularityManager ??= new(_defaultLevel);
            _data = data.Cafe.PopularityManager;
        }

        public bool CheckLevelWaitCritic() {
            return !IsMaxLevel && (_data.Level + 1) % 5 == 0 && _data.Xp == NowLevel.NeedXp;
        }

        public void CriticSuccess() {
            NextLevel();
            RemoveXp(_data.Xp);
        }

        public void CriticFailure() {
            RemoveXp(NowLevel.NeedXp / 2);
        }
    }
}
