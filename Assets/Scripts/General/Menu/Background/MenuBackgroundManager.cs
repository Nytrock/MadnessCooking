using UnityEngine;

public class MenuBackgroundManager : MonoBehaviour {
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private LightManager _lightManager;

    private void Start() {
        GeneralData stubData = new();
        _lightManager.Bind(stubData);
        _timeManager.Bind(stubData);
    }
}
