using System.Collections.Generic;
using UnityEngine;

public class ClientsHolderManager : MonoBehaviour, IBindable<CafeData> {
    [SerializeField] private CafeSpotManager _spotManager;
    [SerializeField] private SpotEditor _spotEditor;
    [SerializeField] private CafeStateChanger _cafeOpener;

    [SerializeField] private List<ClientsHolder> _holders = new();
    private readonly List<List<int>> _freeHolders = new();
    private ClientHolderManagerData _data;

    private void Awake() {
        _spotEditor.EditorDisabled += GenerateFreeHoldersList;
        _spotManager.SpotAdded += AddClientsHolder;
        _spotManager.SpotRemoved += RemoveClientsHolder;
    }

    public void LateStart() {
        GenerateFreeHoldersList();
    }

    private void AddClientsHolder(CafeSpot spot, bool isAddedByEditor) {
        if (!spot.TryGetComponent(out ClientsHolder clientsHolder))
            return;

        clientsHolder.SetIndex(_holders.Count);
        _holders.Add(clientsHolder);
        _cafeOpener.CafeChanged += clientsHolder.CafeStateChanged;

        if (!isAddedByEditor)
            return;

        ClientHolderData newData = new(spot.SeatsCount);
        _data.AddClientHolder(newData);
        clientsHolder.SetData(newData);

    }

    private void RemoveClientsHolder(CafeSpot spot) {
        if (!spot.TryGetComponent(out ClientsHolder clientsHolder))
            return;

        _cafeOpener.CafeChanged -= clientsHolder.CafeStateChanged;
        _data.RemoveClientHolderAt(clientsHolder.Index);
        _holders.Remove(clientsHolder);
    }

    public void GenerateFreeHoldersList() {
        _freeHolders.Clear();
        UpdateIndexes();

        for (int i = 0; i < _spotManager.PrefabsCount; i++)
            _freeHolders.Add(new());
        for (int i = 0; i < _holders.Count; i++)
            _freeHolders[_holders[i].ClientsCount - 1].Add(i);
    }

    public ClientsHolder TakeRandomHolder(ClientCount clientType) {
        int needSeat = clientType switch {
            ClientCount.One => 0,
            ClientCount.Two => 1,
            ClientCount.Three => 2,
            ClientCount.Four => 3,
            _ => 0,
        };

        if (_freeHolders[needSeat].Count == 0)
            return null;

        int randomSpotIndex = _freeHolders[needSeat].GetRandom();
        _freeHolders[needSeat].Remove(randomSpotIndex);
        return _holders[randomSpotIndex];
    }

    public ClientsHolder TakeHolder(int index) {
        _freeHolders[_holders[index].ClientsCount - 1].Remove(index);
        return _holders[index];
    }

    public void ReturnHolder(int index) {
        _freeHolders[_holders[index].ClientsCount - 1].Add(index);
    }

    public void UpdateIndexes() {
        for (int i = 0; i < _holders.Count; i++)
            _holders[i].SetIndex(i);
    }

    public void Bind(CafeData data) {
        data.ClientHolderManager ??= new();
        _data = data.ClientHolderManager;
    }
}
