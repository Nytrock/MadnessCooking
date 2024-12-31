using System;
using UnityEngine;

public class PopularityManager : MonoBehaviour, IBindable<CafeData> {
    [SerializeField] private PopularityLevel[] _levels;
    [SerializeField] private PopularityLevel _defaultLevel;
    [SerializeField] private PopularityManagerData _data;

    private PopularityLevel _nowLevel => _levels[_data.Level];

    public bool IsMaxLevel => _data.Level == _levels.Length - 1;

    public event Action<PopularityLevel> LevelChanged;
    public event Action<int> XpChanged;

    private void Awake() {
        CheckLevels();
    }

    private void CheckLevels() {
        foreach (var level in _levels) {
            int sumChance = level.SingleChance + level.DoubleChance +
                level.TripleChance + level.QuarterChance;
            if (sumChance != 1000)
                throw new ArgumentException($"Popularity level number {level.Number} has incorrect chances");
        }
    }

    public void LateStart() {
        LevelChanged?.Invoke(_nowLevel);
        XpChanged?.Invoke(_data.Xp);
    }

    [ContextMenu("AddXp")]
    void TestAddXp() {
        if (!Application.isPlaying)
            return;

        AddXp(100);
    }

    [ContextMenu("RemoveXp")]
    private void TestRemoveXp() {
        if (!Application.isPlaying)
            return;

        RemoveXp(100);
    }

    public void AddXp(float xp) {
        _data.AddXp((int)xp);

        if (_data.Xp >= _nowLevel.NeedXp && !IsMaxLevel) {
            while (_data.Xp >= _nowLevel.NeedXp && !IsMaxLevel) {
                if ((_data.Level + 1) % 5 == 0) {
                    _data.RemoveXp(_data.Xp - _nowLevel.NeedXp);
                    break;
                }

                _data.RemoveXp(_nowLevel.NeedXp);
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
        LevelChanged?.Invoke(_nowLevel);
    }

    public void PreviousLevel() {
        _data.PreviousLevel();
        LevelChanged?.Invoke(_nowLevel);
        if (_data.Xp >= _nowLevel.NeedXp)
            _data.RemoveXp(_data.Xp - _nowLevel.NeedXp + 1);
    }

    public void Bind(CafeData data) {
        data.PopularityManager ??= new(_defaultLevel);
        _data = data.PopularityManager;
    }

    public bool CheckLevelWaitCritic() {
        return (_data.Level + 1) % 5 == 0 && _data.Xp == _nowLevel.NeedXp;
    }

    public void CriticSuccess() {
        NextLevel();
        RemoveXp(_data.Xp);
    }

    public void CriticFailure() {
        RemoveXp(_nowLevel.NeedXp / 2);
    }
}
