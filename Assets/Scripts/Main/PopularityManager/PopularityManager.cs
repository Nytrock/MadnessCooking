using System;
using UnityEngine;

public class PopularityManager : MonoBehaviour
{
    [SerializeField] private CafeSpaceManager _spaceManager;
    [SerializeField] private ClientTimeMultiplier _timeMultiplier;
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

    public float GetPopularity()
    {
        return _levels[_nowLevel].PopularityMultiplier * _timeMultiplier.DaytimeMultiplier;
    }

    public void GetClientsNumberChances(out int singleChance, out int doubleChance, out int tripleChance, out int quarterChance)
    {
        singleChance = (int)(_levels[_nowLevel].SingleChance * 10);
        doubleChance = (int)(_levels[_nowLevel].DoubleChance * 10);
        tripleChance = (int)(_levels[_nowLevel].TripleChance * 10);
        quarterChance = (int)(_levels[_nowLevel].QuarterChance * 10);

        doubleChance += singleChance;
        tripleChance += doubleChance;
        quarterChance += tripleChance;
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
            NextLevel();
            _nowXp = 0;
        }
        XpChanged?.Invoke(_nowXp);
    }

    private void NextLevel()
    {
        _nowLevel++;
        LevelChanged?.Invoke(_levels[_nowLevel]);

        if (_levels.Length == _nowLevel + 1) {
            _isMaxLevel = true;
        }
    }

    public float GetSpaceMultiplier()
    {
        return Mathf.Max(1, _spaceManager.SpaceCount * 0.375f);
    }
}
