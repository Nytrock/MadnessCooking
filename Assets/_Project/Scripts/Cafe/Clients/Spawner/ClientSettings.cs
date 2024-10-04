using System;

[Serializable]
public struct ClientSettings {
    public ClientData Data { get; private set; }
    public int SpotIndex { get; private set; }
    public int SeatIndex { get; private set; }
    public ClientsSpawner Spawner { get; private set; }

    public ClientSettings(ClientData data, int spotIndex, int seatIndex, ClientsSpawner spawner) {
        Data = data;
        SpotIndex = spotIndex;
        SeatIndex = seatIndex;
        Spawner = spawner;
    }
}
