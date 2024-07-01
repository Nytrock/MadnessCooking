using System;
using UnityEngine;

public class PopularityManager : MonoBehaviour, IBindable<GeneralData> {
    [SerializeField] private PopularityLevel[] _levels;
    private PopularityManagerData _data;

    public int NowLevel => _data.Level;
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
        LevelChanged?.Invoke(_levels[_data.Level]);
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

    public void AddXp(int xp) {
        _data.AddXp(xp);
        if (_data.Xp >= _levels[_data.Level].NeedXp && !_data.IsMaxLevel) {
            while (_data.Xp >= _levels[_data.Level].NeedXp && !_data.IsMaxLevel) {
                _data.RemoveXp(_levels[_data.Level].NeedXp);
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
        LevelChanged?.Invoke(_levels[_data.Level]);
    }

    public void PreviousLevel() {
        _data.PreviousLevel();
        LevelChanged?.Invoke(_levels[_data.Level]);
        if (_data.Xp >= _levels[_data.Level].NeedXp)
            _data.RemoveXp(_data.Xp - _levels[_data.Level].NeedXp + 1);
    }

    public void Bind(GeneralData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.PopularityManager = new();
        _data = data.PopularityManager;
        LateStart();
    }
}
