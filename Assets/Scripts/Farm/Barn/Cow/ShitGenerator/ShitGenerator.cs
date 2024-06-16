using UnityEngine;

public class ShitGeneraor : MonoBehaviour, IBindable<FarmData> {
    [SerializeField] private Puncher _puncher;
    [SerializeField, Min(0)] private float _needTime;
    private ShitGeneratorData _data;

    private void Update() {
        if (_data.NowTime < _needTime) {
            _data.AddTime();
        } else {
            _puncher.AddMaterial();
            _data.ResetTime();
        }
    }

    public void Bind(FarmData data, bool isFileEmpty) {
        if (isFileEmpty)
            data.ShitGenerator = new();
        _data = data.ShitGenerator;
    }
}
