using System;
using System.Collections.Generic;
using UnityEngine;

public class CafeSpotManager : MonoBehaviour, IBindable<CafeData> {
    [SerializeField] private CafeSpaceManager _spaceManager;
    [SerializeField] private AudioSource _removeButtonAudio;
    [SerializeField] private CafeSpot[] _spotPrefabs;

    private readonly List<CafeSpot> _spots = new();
    private ClientHolderManagerData _data;
    private float _cellSize;

    public float CellSize => _cellSize;
    public float PrefabsCount => _spotPrefabs.Length;

    public event Action<float> SpotsPositionChanged;
    public event Action<CafeSpot, bool> SpotAdded;
    public event Action<CafeSpot> SpotRemoved;

    private void Awake() {
        _cellSize = _spaceManager.SpaceSize / 2f;
    }

    public void LateStart() {
        GenerateSpots();
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

    private void RemoveSpot(int spotIndex) {
        float offset = _cellSize * _spots[spotIndex].SeatsCount;
        SpotsPositionChanged?.Invoke(-offset);
        MoveSpots(spotIndex, offset);
        SpotRemoved?.Invoke(_spots[spotIndex]);

        _spots[spotIndex].Destroy();
        _spots.RemoveAt(spotIndex);
        SetupSpotsRemoveButtons();
    }

    private void MoveSpots(int deletedIndex, float deletedSize) {
        for (int i = deletedIndex + 1; i < _spots.Count; i++)
            _spots[i].transform.position -= new Vector3(deletedSize, 0, 0);
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
        SpotAdded?.Invoke(spot, isAddedByEditor);

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
}
