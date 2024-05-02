using UnityEngine;

public abstract class TimeRenderer : MonoBehaviour
{
    [SerializeField] protected TimeManager _timeManager;

    private void Update()
    {
        UpdateVisual();
    }

    protected abstract void UpdateVisual();
}
