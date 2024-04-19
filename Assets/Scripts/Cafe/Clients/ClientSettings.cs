using System;

[Serializable]
public struct ClientSettings
{
    public ClientSettings(SerializableClient data, int spotIndex, int tableIndex, ClientsSpawner spawner)
    {
        Data = data;
        SpotIndex = spotIndex;
        TableIndex = tableIndex;
        Spawner = spawner;
    }

    public SerializableClient Data { get; private set; }
    public int SpotIndex { get; private set; }
    public int TableIndex { get; private set; }
    public ClientsSpawner Spawner { get; private set; }
}
