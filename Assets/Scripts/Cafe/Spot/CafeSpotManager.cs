using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CafeSpotManager : MonoBehaviour, IBindable<CafeData>
{
    [SerializeField] private CafeSpaceManager _spaceManager;
    [SerializeField] private CafeOpener _opener;
    [SerializeField] private CafeSpot[] _spotPrefabs;

    private readonly List<CafeSpot> _spots = new();
    private List<List<int>> _freeSpots;
    private float _cellSize;
    private CafeData _data;

    public event Action<float> SpotsPositionChanged;

    private void Awake()
    {
        _freeSpots = new(_spotPrefabs.Length);
    }

    private void LateStart()
    {
        _cellSize = _spaceManager.SpaceData.SpaceSize / 2f;
        GenerateSpots();
        GenerateFreeSpotsList();
    }

    private void SetupSpotsRemoveButtons()
    {
        for (int i = 0; i < _spots.Count; i++)
            SetupSpotRemoveButton(i);
    }

    private void SetupSpotRemoveButton(int i)
    {
        Button.ButtonClickedEvent eventHandler = _spots[i].RemoveButton.onClick;
        eventHandler.RemoveAllListeners();
        eventHandler.AddListener(delegate { RemoveSpot(i); });
    }

    private void GenerateSpots()
    {
        for (int i = 0; i < _data.Spots.Count; i++)
            AddNewSpot(_data.Spots[i].SeatsCount - 1, false);
    }

    public void GenerateFreeSpotsList()
    {
        _freeSpots.Clear();
        for (int i = 0; i < _spotPrefabs.Length; i++)
            _freeSpots.Add(new());
        for (int i = 0; i < _spots.Count; i++)
            _freeSpots[_spots[i].SeatsCount - 1].Add(i);
    }

    private void RemoveSpot(int spotIndex)
    {
        float offset = _cellSize * _spots[spotIndex].SeatsCount;
        SpotsPositionChanged?.Invoke(-offset);

        MoveSpots(spotIndex, offset);
        if (_spots[spotIndex].TryGetComponent(out ClientGroupHolder clientTable))
            _opener.CafeChanged -= clientTable.CafeClosed;
        _spots[spotIndex].Destroy();
        _spots.RemoveAt(spotIndex); 
        _data.Spots.RemoveAt(spotIndex);
        SetupSpotsRemoveButtons();
    }

    private void MoveSpots(int deletedIndex, float deletedSize)
    {
        for (int i = deletedIndex + 1; i < _spots.Count; i++)
            _spots[i].transform.position -= new Vector3(deletedSize, 0, 0);
    }

    public int TakeRandomSpot(ClientCount clientType)
    {
        int needSeat = clientType switch {
            ClientCount.One => 0,
            ClientCount.Two => 1,
            ClientCount.Three => 2,
            ClientCount.Four => 3,
            _ => 0,
        };

        if (_freeSpots[needSeat].Count == 0)
            return -1;

        int randomSpotIndex = _freeSpots[needSeat][Random.Range(0, _freeSpots[needSeat].Count)];
        _freeSpots[needSeat].Remove(randomSpotIndex);
        return randomSpotIndex;
    }

    public void TakeSpot(int index)
    {
        _freeSpots[_spots[index].SeatsCount - 1].Remove(index);
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

    public float GetLengthOfAllSpots()
    {
        float size = 0;
        foreach (var spot in _spots)
            size += spot.SeatsCount;
        return size * _cellSize;
    }

    public int GetFreeSpace()
    {
        int freeSpace = _spaceManager.SpaceData.Count * 2;
        foreach (var spot in _spots)
            freeSpace -= spot.SeatsCount;
        return freeSpace;
    }

    public void AddNewSpot(int index, bool isAddedByEditor = true)
    {
        CafeSpot spot = Instantiate(_spotPrefabs[index], transform);
        spot.ChangeEditorState(isAddedByEditor);
        spot.SetIndex(_spots.Count);
        if (isAddedByEditor)
            _data.Spots.Add(new SpotData(spot.SeatsCount));
        if (spot.TryGetComponent(out ClientGroupHolder clientTable)) {
            _opener.CafeChanged += clientTable.CafeClosed;
            clientTable.SetData(_data.Spots[spot.Index]);
        }

        float offset = _cellSize;
        switch (spot.SeatsCount) {
            case 1: offset *= -0.5f; break;
            case 2: offset *= 0; break;
            case 3: offset *= 0.5f; break;
        }
        spot.transform.position += new Vector3(GetLengthOfAllSpots() + offset, 0, 0);

        _spots.Add(spot);
        SetupSpotRemoveButton(_spots.Count - 1);
        SpotsPositionChanged?.Invoke(_cellSize * spot.SeatsCount);
    }

    public void Bind(CafeData data, bool isFileEmpty)
    {
        _data = data;
        if (isFileEmpty) {
            foreach (var spot in _spots) {
                _data.Spots.Add(new SpotData(spot.SeatsCount));
            }
        }

        LateStart();
    }

    public CafeSpot GetSpotByIndex(int spotIndex) => _spots[spotIndex];
}
