using System;
using UnityEngine;

public class PopularityManager : MonoBehaviour, IBindable<MainData>
{
    [SerializeField] private PopularityLevel[] _levels;
    private int _nowLevel = 0;
    private int _nowXp = 0;

    private bool _isMaxLevel;
    private MainData _data;

    public int NowLevel => _nowLevel;
    public bool IsMaxLevel => _isMaxLevel;

    public event Action<PopularityLevel> LevelChanged;
    public event Action<int> XpChanged;

    private void LateStart()
    {
        LevelChanged?.Invoke(_levels[_nowLevel]);
    }

    [ContextMenu("AddXp")]
    void TextAddXp()
    {
        AddXp(10);
    }

    public void AddXp(int xp)
    {
        _nowXp += xp;
        if (_nowXp >= _levels[_nowLevel].NeedXp && !_isMaxLevel) {
            while (_nowXp >= _levels[_nowLevel].NeedXp && !_isMaxLevel) {
                _nowXp -= _levels[_nowLevel].NeedXp;
                NextLevel();
            }
        }
        _data.PopularityXp = _nowXp;
        XpChanged?.Invoke(_nowXp);
    }

    public void RemoveXp(int xp)
    {
        _nowXp = Mathf.Max(0, _nowXp - xp);
        _data.PopularityXp = _nowXp;
        XpChanged?.Invoke(_nowXp);
    }

    public void NextLevel()
    {
        _nowLevel++;
        _data.PopularityLevel = _nowLevel;
        LevelChanged?.Invoke(_levels[_nowLevel]);

        if (_levels.Length == _nowLevel + 1) {
            _isMaxLevel = true;
        }
    }

    public void PreviousLevel()
    {
        _nowLevel--;
        _data.PopularityLevel = _nowLevel;
        LevelChanged?.Invoke(_levels[_nowLevel]);
        _isMaxLevel = false;

        if (_nowXp >= _levels[_nowLevel].NeedXp) {
            _nowXp = _levels[_nowLevel].NeedXp / 2;
            _data.PopularityXp = _nowXp;
        }
    }

    public void Bind(MainData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty) {
            _data.PopularityXp = _nowXp;
            _data.PopularityLevel = _nowLevel;
            LateStart();
            return;
        }

        int levelsCount = _data.PopularityLevel;
        int xp  =_data.PopularityXp;
        for (int i = 0; i < levelsCount; i++) {
            AddXp(_levels[i].NeedXp);
        }
        AddXp(xp);
        LateStart();
    }
}
