using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FatigueSourceButton : MonoBehaviour {
    [SerializeField, Min(0)] private float _fatigueCoef;

    private void Awake() {
        GetComponent<Button>().onClick.AddListener(IncreaseFatigue);
    }

    private void IncreaseFatigue() {
        FatigueManager.Instance.AddFatigue(_fatigueCoef);
    }
}
