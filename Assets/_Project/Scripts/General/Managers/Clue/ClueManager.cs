using System;
using UnityEngine;

public class ClueManager : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private ClueHole _hole;
    [SerializeField] private ClueRenderer _renderer;

    public event Action ClueHided;

    private void Awake() {
        _panel.SetActive(false);
    }

    public void ShowClue(ClueTemplate template) {
        _hole.ChangeTransform(template);
        _renderer.StartRenderClue(template);
        _panel.SetActive(true);
    }

    public void HideClue() {
        _panel.SetActive(false);
        ClueHided?.Invoke();
    }
}
