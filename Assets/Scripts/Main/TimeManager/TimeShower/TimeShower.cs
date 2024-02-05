using UnityEngine;

public class TimeShower : MonoBehaviour
{
    [SerializeField] protected TimeManager _timeManager;

    private void Update()
    {
        UpdateVisual();
    }

    protected virtual void UpdateVisual()
    {

    }
}
