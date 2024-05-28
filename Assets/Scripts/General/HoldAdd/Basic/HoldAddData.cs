using System;

[Serializable]
public class HoldAddData {
    public float NowTime;
    public bool IsUnlocked;
    public bool IsAuto;
    public float Speed = 1;
    public int ReadyCount;
}
