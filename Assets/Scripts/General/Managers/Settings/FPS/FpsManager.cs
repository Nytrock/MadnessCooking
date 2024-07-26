using System.Linq;
using UnityEngine;

public class FpsManager : MonoBehaviour {
    [SerializeField] private int _targetFrameRate = 60;
    private int _lastFrameIndex;
    private float[] _frameDeltaTimeArray;

    private void Awake() {
        Application.targetFrameRate = _targetFrameRate;
        _frameDeltaTimeArray = new float[_targetFrameRate];
    }

    private void Update() {
        _frameDeltaTimeArray[_lastFrameIndex] = Time.deltaTime;
        _lastFrameIndex = (_lastFrameIndex + 1) % _targetFrameRate;
    }

    public float GetFPS() {
        return 1 / (_frameDeltaTimeArray.Sum() / _targetFrameRate);
    }
}
