using UnityEngine;

public abstract class TimeRenderer : MonoBehaviour {
    [SerializeField] protected GameTimeManager _timeManager;

    private void Update() {
        UpdateVisual();
    }

    protected abstract void UpdateVisual();
}
