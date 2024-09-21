using System;
using UnityEngine;

public class ClueManager : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private ClueHole _hole;
    [SerializeField] private ClueRenderer _renderer;
    private bool _isWork;

    public event Action ClueHided;

    private void Awake() {
        _panel.SetActive(false);
        ChangeWorkState(false);
    }

    public void ShowClue(ClueTemplate template) {
        if (!_isWork)
            return;

        _hole.ChangeTransform(template);
        _renderer.RenderClue(template);
        _panel.SetActive(true);
    }

    public void HideClue() {
        if (!_isWork)
            return;

        _panel.SetActive(false);
        ClueHided?.Invoke();
    }

    public void ChangeWorkState(bool isWork) {
        _isWork = isWork;
    }
}
