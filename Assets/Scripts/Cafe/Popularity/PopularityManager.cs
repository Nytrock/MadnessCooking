using System;
using UnityEngine;

public class PopularityManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField] private PopularityLevel[] _levels;
    private PopularityManagerData _data;

    private PopularityLevel _nowLevel => _levels[_data.Level];

    public bool IsMaxLevel => _data.IsMaxLevel;

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
                throw new ArgumentException($"Popularity level {level.Name} has incorrect chances");
        }
    }

    private void LateStart() {
        LevelChanged?.Invoke(_nowLevel);
        XpChanged?.Invoke(_data.Xp);
    }

    [ContextMenu("AddXp")]
    void TestAddXp() {
        AddXp(100);
        Debug.Log(_data.Xp);
    }

    [ContextMenu("RemoveXp")]
    private void TestRemoveXp() {
        RemoveXp(100);
        Debug.Log(_data.Xp);
    }

    public void AddXp(float xp) {
        _data.AddXp((int)xp);

        if (_data.Xp >= _nowLevel.NeedXp && !_data.IsMaxLevel) {
            while (_data.Xp >= _nowLevel.NeedXp && !_data.IsMaxLevel) {
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

    public void RemoveXp(int xp) {
        _data.RemoveXp(xp);
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
        _data.NextLevel(_levels.Length);
        LevelChanged?.Invoke(_nowLevel);
    }

    public void PreviousLevel() {
        _data.PreviousLevel();
        LevelChanged?.Invoke(_nowLevel);
        if (_data.Xp >= _nowLevel.NeedXp)
            _data.RemoveXp(_data.Xp - _nowLevel.NeedXp + 1);
    }

    public void Bind(GeneralData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.PopularityManager = new();
        _data = data.PopularityManager;
        LateStart();
    }

    public bool CheckLevelWaitCritic() {
        return (_data.Level + 1) % 5 == 0 && _data.Xp == _nowLevel.NeedXp;
    }

    public void CriticFailure() {
        RemoveXp(_nowLevel.NeedXp / 2);
    }
}
