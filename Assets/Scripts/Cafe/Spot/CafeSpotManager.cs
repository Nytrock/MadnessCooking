using System;
using System.Collections.Generic;
using UnityEngine;

public class CafeSpotManager : MonoBehaviour
{
    [SerializeField] private CafeSpaceManager _spaceManager;
    [SerializeField] private CafeOpener _opener;
    [SerializeField] private CafeSpot[] _spotPrefabs;
    private readonly List<CafeSpot> _spots = new List<CafeSpot>();
    private readonly List<List<int>> _freeSpots = new List<List<int>>(4);
    private float _cellSize;

    public event Action<float> SpotsPositionChanged;

    private void Start()
    {
        GenerateSpots();
        SetupSpotsRemoveButtons();
        GenerateFreeSpotsList();
        _cellSize = _spaceManager.GetSpaceSize() / 2f;
    }

    private void SetupSpotsRemoveButtons()
    {
        for (int i = 0; i < _spots.Count; i++)
            SetUpSpotRemoveButton(i);
    }

    private void SetUpSpotRemoveButton(int i)
    {
        var eventHandler = _spots[i].RemoveButton.onClick;
        eventHandler.RemoveAllListeners();
        eventHandler.AddListener(delegate { RemoveSpot(i); });
    }

    private void GenerateSpots()
    {
        for (int i = 0; i < _spots.Count; i++) {
            if (_spots[i].TryGetComponent(out ClientGroupHolder clientTable))
                _opener.CafeChanged -= clientTable.CafeClosed;
        }

        // Генерируем споты на основе списка
    }

    public void GenerateFreeSpotsList()
    {
        _freeSpots.Clear();
        for (int i = 0; i < 4; i++)
            _freeSpots.Add(new List<int>());
        for (int i = 0; i < _spots.Count; i++)
            _freeSpots[_spots[i].SeatsCount - 1].Add(i);
    }

    private void RemoveSpot(int spotIndex)
    {
        var offset = _cellSize * _spots[spotIndex].SeatsCount;
        SpotsPositionChanged?.Invoke(-offset);

        MoveSpots(spotIndex, offset);
        if (_spots[spotIndex].TryGetComponent(out ClientGroupHolder clientTable))
            _opener.CafeChanged -= clientTable.CafeClosed;
        _spots[spotIndex].Destroy();
        _spots.RemoveAt(spotIndex);
        SetupSpotsRemoveButtons();
    }

    private void MoveSpots(int deletedIndex, float deletedSize)
    {
        for (int i = deletedIndex + 1; i < _spots.Count; i++)
            _spots[i].transform.position -= new Vector3(deletedSize, 0, 0);
    }

    public CafeSpot GetRandomSpot(ClientType clientType)
    {
        int needSeat;
        switch (clientType)
        {
            case ClientType.Double:
                needSeat = 2;
                break;
            case ClientType.Triple:
                needSeat = 3;
                break;
            case ClientType.Quarter:
                needSeat = 4;
                break;
            default:
                needSeat = 1;
                break;
        }
        needSeat -= 1;

        if (_freeSpots[needSeat].Count == 0)
            return null;

        var randomSpotNum = _freeSpots[needSeat][UnityEngine.Random.Range(0, _freeSpots[needSeat].Count)];
        _freeSpots[needSeat].Remove(randomSpotNum);
        return _spots[randomSpotNum];
    }

    public bool CheckHavingSpots()
    {
        int res = 0;
        for (int i = 0; i < _freeSpots.Count; i++)
            res += _freeSpots[i].Count;
        return res != 0;
    }

    public void ReturnSpot(int index)
    {
        _freeSpots[_spots[index].SeatsCount - 1].Add(index);
    }

    public void ActivateSpotsEditor()
    {
        foreach (var spot in _spots)
            spot.ChangeEditorState(true);
    }

    public void DisableSpotsEditor()
    {
        foreach (var spot in _spots)
            spot.ChangeEditorState(false);
    }

    public float GetLengthAllSpots()
    {
        float size = 0;
        foreach (var spot in _spots)
            size += spot.SeatsCount;
        return size * _cellSize;
    }

    public int GetFreeSpace()
    {
        int freeSpace = _spaceManager.SpaceCount * 2;
        foreach (var spot in _spots)
            freeSpace -= spot.SeatsCount;
        return freeSpace;
    }

    public void AddNewSpot(int index)
    {
        var spot = Instantiate(_spotPrefabs[index], transform);
        spot.ChangeEditorState(true);
        spot.SetIndex(_spots.Count);
        if (spot.TryGetComponent(out ClientGroupHolder clientTable))
            _opener.CafeChanged += clientTable.CafeClosed;

        float offset = _cellSize;
        switch (spot.SeatsCount) {
            case 1: offset *= -0.5f; break;
            case 2: offset *= 0; break;
            case 3: offset *= 0.5f; break;
        }
        spot.transform.position += new Vector3(GetLengthAllSpots() + offset, 0, 0);

        _spots.Add(spot);
        SetUpSpotRemoveButton(_spots.Count - 1);
        SpotsPositionChanged?.Invoke(_cellSize * spot.SeatsCount);
    }
}
