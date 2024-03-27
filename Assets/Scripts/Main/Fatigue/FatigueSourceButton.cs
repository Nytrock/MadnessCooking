using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FatigueSourceButton : MonoBehaviour
{
    [SerializeField, Min(0)] private float _fatigueCoef;
    private FatigueManager _manager;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(IncreaseFatigue);
        _manager = FatigueManager.instance;
    }

    private void IncreaseFatigue()
    {
        _manager.ChangeFatigue(_fatigueCoef);
    }
}
