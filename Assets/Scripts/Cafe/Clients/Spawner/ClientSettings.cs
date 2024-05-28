using System;

[Serializable]
public struct ClientSettings {
    public ClientData Data { get; private set; }
    public int SpotIndex { get; private set; }
    public int TableIndex { get; private set; }
    public ClientsSpawner Spawner { get; private set; }

    public ClientSettings(ClientData data, int spotIndex, int tableIndex, ClientsSpawner spawner) {
        Data = data;
        SpotIndex = spotIndex;
        TableIndex = tableIndex;
        Spawner = spawner;
    }
}
