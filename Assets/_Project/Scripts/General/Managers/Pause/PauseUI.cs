using UnityEngine;

public class PauseUI : MonoBehaviour {
    [SerializeField] private PauseManager _manager;
    [SerializeField] private GameObject _panel;

    private void Awake() {
        _manager.PauseChanged += ChangeState;
    }

    private void ChangeState(bool newState) {
        _panel.SetActive(newState);
    }
}
