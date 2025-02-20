using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CafeSpotManager : MonoBehaviour, IBindable<CafeData> {
    [SerializeField] private CafeSpaceManager _spaceManager;
    [SerializeField] private HorizontalCameraManager _cameraManager;
    [SerializeField] private ClientsSpawner _spawner;
    [SerializeField] private CafeStateChanger _opener;
    [SerializeField] private AudioSource _removeButtonAudio;
    [SerializeField] private CafeSpot[] _spotPrefabs;

    private readonly List<CafeSpot> _spots = new();
    private List<List<int>> _freeSpots;
    private float _cellSize;
    [SerializeField] private ClientHolderManagerData _data;

    public float CellSize => _cellSize;

    public event Action<float> SpotsPositionChanged;

    private void Awake() {
        _freeSpots = new(_spotPrefabs.Length);
        _cellSize = _spaceManager.SpaceSize / 2f;
    }

    public void LateStart() {
        GenerateSpots();
        GenerateFreeSpotsList();
    }

    private void SetupSpotsRemoveButtons() {
        for (int i = 0; i < _spots.Count; i++)
            SetupSpotRemoveButton(i);
    }

    private void SetupSpotRemoveButton(int i) {
        _spots[i].SetupRemoveButton(delegate { RemoveSpot(i); }, _removeButtonAudio);
    }

    private void GenerateSpots() {
        foreach (var spotData in _data.ClientHolders)
            AddNewSpot(spotData.SeatsCount - 1, false);
    }

    public void GenerateFreeSpotsList() {
        _freeSpots.Clear();
        UpdateIndexes();

        for (int i = 0; i < _spotPrefabs.Length; i++)
            _freeSpots.Add(new());
        for (int i = 0; i < _spots.Count; i++)
            _freeSpots[_spots[i].SeatsCount - 1].Add(i);
    }

    private void RemoveSpot(int spotIndex) {
        if (!_spots[spotIndex].CanRemove)
            return;

        float offset = _cellSize * _spots[spotIndex].SeatsCount;
        SpotsPositionChanged?.Invoke(-offset);
        MoveSpots(spotIndex, offset);

        if (_spots[spotIndex].TryGetComponent(out ClientsHolder clientTable))
            _opener.CafeChanged -= clientTable.CafeStateChanged;
        _spots[spotIndex].Destroy();
        _spots.RemoveAt(spotIndex);
        _data.RemoveClientHolderAt(spotIndex);
        SetupSpotsRemoveButtons();
    }

    private void MoveSpots(int deletedIndex, float deletedSize) {
        for (int i = deletedIndex + 1; i < _spots.Count; i++)
            _spots[i].transform.position -= new Vector3(deletedSize, 0, 0);
    }

    public int TakeRandomSpot(ClientCount clientType) {
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

    public void TakeSpot(int index) {
        _freeSpots[_spots[index].SeatsCount - 1].Remove(index);
    }

    public bool CheckHavingSpots() {
        int res = 0;
        for (int i = 0; i < _freeSpots.Count; i++)
            res += _freeSpots[i].Count;
        return res != 0;
    }

    public void ReturnSpot(int index) {
        _freeSpots[_spots[index].SeatsCount - 1].Add(index);
    }

    public void ActivateSpotsEditor() {
        foreach (var spot in _spots)
            spot.ChangeEditorState(true);
    }

    public void DisableSpotsEditor() {
        foreach (var spot in _spots)
            spot.ChangeEditorState(false);
    }

    public float GetLengthOfAllSpots() {
        float size = 0;
        foreach (var spot in _spots)
            size += spot.SeatsCount;
        return size * _cellSize;
    }

    public int GetFreeSpace() {
        int freeSpace = _spaceManager.SpaceCount * 2;
        foreach (var spot in _spots)
            freeSpace -= spot.SeatsCount;
        return freeSpace;
    }

    public void AddNewSpot(int index, bool isAddedByEditor = true) {
        CafeSpot spot = Instantiate(_spotPrefabs[index], transform);
        spot.ChangeEditorState(isAddedByEditor);
        spot.SetIndex(_spots.Count);
        spot.SetupOnCreate(_cameraManager);

        ClientHolderData newData = new(spot.SeatsCount);
        if (isAddedByEditor)
            _data.AddClientHolder(newData);
        if (spot.TryGetComponent(out ClientsHolder clientTable)) {
            _opener.CafeChanged += clientTable.CafeStateChanged;
            clientTable.SetData(newData);
            clientTable.SetSpawner(_spawner);
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

    public void Bind(CafeData data) {
        data.ClientHolderManager ??= new();
        _data = data.ClientHolderManager;
    }

    public CafeSpot GetSpotByIndex(int spotIndex) => _spots[spotIndex];

    public void UpdateIndexes() {
        for (int i = 0; i < _spots.Count; i++)
            _spots[i].SetIndex(i);
    }
}
