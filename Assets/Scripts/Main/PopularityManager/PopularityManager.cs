using System;
using UnityEngine;

public class PopularityManager : MonoBehaviour
{
    [SerializeField] private PopularityLevel[] _levels;
    private int _nowLevel;
    private int _nowXp;

    private bool _isMaxLevel;

    public int NowLevel => _nowLevel;
    public bool IsMaxLevel => _isMaxLevel;

    public event Action<PopularityLevel> LevelChanged;
    public event Action<int> XpChanged;

    private void Start()
    {
        LevelChanged?.Invoke(_levels[_nowLevel]);
    }

    [ContextMenu("AddXp")]
    public void TextAddXp()
    {
        AddXp(10);
    }

    public void AddXp(int xp)
    {
        _nowXp += xp;
        if (_nowXp >= _levels[_nowLevel].NeedXp && !_isMaxLevel) {
            while (_nowXp >= _levels[_nowLevel].NeedXp) {
                _nowXp -= _levels[_nowLevel].NeedXp;
                NextLevel();
            }
        }
        XpChanged?.Invoke(_nowXp);
    }

    public void RemoveXp(int xp)
    {
        _nowXp = Mathf.Max(0, _nowXp - xp);
    }

    public void NextLevel()
    {
        _nowLevel++;
        LevelChanged?.Invoke(_levels[_nowLevel]);

        if (_levels.Length == _nowLevel + 1) {
            _isMaxLevel = true;
        }
    }

    public void PreviousLevel()
    {
        _nowLevel--;
        LevelChanged?.Invoke(_levels[_nowLevel]);
        _isMaxLevel = false;

        if (_nowXp >= _levels[_nowLevel].NeedXp)
            _nowXp = _levels[_nowLevel].NeedXp / 2;
    }
}
