using System;
using UnityEngine;

public class SpotPreview : MonoBehaviour {
    [SerializeField] private CafeSpot[] _spotsPreviews;
    private int _nowPreview = -1;

    private void Awake() {
        DisableSpots();
        for (int i = 0; i < _spotsPreviews.Length; i++) {
            if (i + 1 != _spotsPreviews[i].SeatsCount) {
                throw new ArgumentException($"Spot preview is in the wrong position " +
                    $"({i}, but should be {_spotsPreviews[i].SeatsCount - 1})");
            }

            _spotsPreviews[i].ChangeEditorState(true);
            _spotsPreviews[i].RemoveButton.gameObject.SetActive(false);
            _spotsPreviews[i].enabled = false;
        }
    }

    public void ChangePreview(int newPreview) {
        if (_nowPreview != -1)
            _spotsPreviews[_nowPreview].gameObject.SetActive(false);

        _nowPreview = newPreview;
        if (_nowPreview != -1)
            _spotsPreviews[_nowPreview].gameObject.SetActive(true);
    }

    private void DisableSpots() {
        foreach (var spot in _spotsPreviews)
            spot.gameObject.SetActive(false);
    }

    public void Move(float offset) {
        transform.position += new Vector3(offset, 0, 0);
    }
}
