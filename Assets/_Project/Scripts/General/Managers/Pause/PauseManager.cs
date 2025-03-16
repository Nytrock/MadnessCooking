using System;
using UnityEngine;

public class PauseManager : Singleton<PauseManager> {
    private bool _isPause = false;

    public bool IsPause => _isPause;

    public event Action<bool> PauseChanged;

    private void Start() {
        ChangePauseState(false);
    }

    private void Update() {
        if (!ScenesManager.IsGame())
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            ChangePauseState();
    }

    private void ChangePauseState() {
        ChangePauseState(!_isPause);
    }

    private void ChangePauseState(bool newState) {
        _isPause = newState;
        Time.timeScale = Convert.ToInt32(!newState);
        PauseChanged?.Invoke(newState);
    }
}
