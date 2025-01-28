using System;

[Serializable]
public struct ClientSettings {
    public ClientData Data { get; private set; }
    public int SpotIndex { get; private set; }
    public int SeatIndex { get; private set; }

    public ClientSettings(ClientData data, int spotIndex, int seatIndex) {
        Data = data;
        SpotIndex = spotIndex;
        SeatIndex = seatIndex;
    }
}
