using UnityEngine;

public class ShitGeneraor : MonoBehaviour
{
    [SerializeField] private Puncher _puncher;
    [SerializeField] private float _needTime;
    private float _nowTime = 0;

    private void Update()
    {
        if (_nowTime < _needTime) {
            _nowTime += Time.deltaTime * TimeManager.instance.TimeSpeed;
        } else {
            _puncher.AddShit();
            _nowTime = 0;
        }
    }
}
