using System;
using UnityEngine;

public class Puncher : MonoBehaviour, IBindable<FarmData> {
    private PuncherData _data;

    public event Action FertilizeChanged;

    public void Bind(FarmData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.Puncher = new();
        _data = data.Puncher;
    }

    public void SubtractFertilizer() {
        _data.SubtractFertilizer();
    }
}
