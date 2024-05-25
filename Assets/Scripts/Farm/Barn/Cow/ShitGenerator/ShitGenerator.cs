using UnityEngine;

public class ShitGeneraor : MonoBehaviour, IBindable<FarmData>
{
    [SerializeField] private Puncher _puncher;
    [SerializeField, Min(0)] private float _needTime;
    private ShitGeneratorData _data;

    private void Update()
    {
        if (_data.NowTime < _needTime) {
            _data.NowTime += TimeManager.Instance.InGameTimeSpeed;
        } else {
            _puncher.AddShit();
            _data.NowTime = 0;
        }
    }

    public void Bind(FarmData data, bool isFileEmpty)
    {
        _data = data.ShitGenerator;
    }
}
