using System;
using UnityEngine;

public class PopularityManager : MonoBehaviour, IBindable<GeneralData>
{
    [SerializeField] private PopularityLevel[] _levels;
    private bool _isMaxLevel;
    private GeneralData _data;

    public int NowLevel => _data.PopularityLevel;
    public bool IsMaxLevel => _isMaxLevel;

    public event Action<PopularityLevel> LevelChanged;
    public event Action<int> XpChanged;

    private void LateStart()
    {
        LevelChanged?.Invoke(_levels[_data.PopularityLevel]);
    }

    [ContextMenu("AddXp")]
    void TestAddXp()
    {
        AddXp(80);
    }

    [ContextMenu("RemoveXp")]
    void TestRemoveXp()
    {
        RemoveXp(70);
    }

    public void AddXp(int xp)
    {
        _data.PopularityXp += xp;
        if (_data.PopularityXp >= _levels[_data.PopularityLevel].NeedXp && !_isMaxLevel) {
            while (_data.PopularityXp >= _levels[_data.PopularityLevel].NeedXp && !_isMaxLevel) {
                _data.PopularityXp -= _levels[_data.PopularityLevel].NeedXp;
                NextLevel();
            }
        }
        XpChanged?.Invoke(_data.PopularityXp);
    }

    public void RemoveXp(int xp)
    {
        if (_data.PopularityLevel == 0)
            _data.PopularityXp = Mathf.Max(0, _data.PopularityXp - xp);
        else
            _data.PopularityXp -= xp;

        if (_data.PopularityXp < 0 && _data.PopularityLevel > 0) {
            while (_data.PopularityXp < 0 && _data.PopularityLevel > 0) {
                _data.PopularityXp += _levels[_data.PopularityLevel - 1].NeedXp;
                PreviousLevel();
            }
        }
        XpChanged?.Invoke(_data.PopularityXp);
    }

    public void NextLevel()
    {
        _data.PopularityLevel++;
        LevelChanged?.Invoke(_levels[_data.PopularityLevel]);

        if (_levels.Length == _data.PopularityLevel + 1) {
            _isMaxLevel = true;
        }
    }

    public void PreviousLevel()
    {
        _data.PopularityLevel--;
        LevelChanged?.Invoke(_levels[_data.PopularityLevel]);
        _isMaxLevel = false;

        if (_data.PopularityXp >= _levels[_data.PopularityLevel].NeedXp) {
            _data.PopularityXp = _levels[_data.PopularityLevel].NeedXp / 2;
        }
    }

    public void Bind(GeneralData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty) {
            LateStart();
            return;
        }

        int xp = _data.PopularityXp;
        for (int i = 0; i < _data.PopularityLevel; i++) {
            AddXp(_levels[i].NeedXp);
        }
        AddXp(xp);
        LateStart();
    }
}
