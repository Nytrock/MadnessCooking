using UnityEngine;

public class SpotPreview : MonoBehaviour
{
    [SerializeField] private CafeSpot[] _spotsPreviews;
    private int _nowPreview = -1;

    private void Start()
    {
        DisableSpots();
        foreach (var spot in _spotsPreviews) {
            spot.ChangeEditorState(true);
            spot.RemoveButton.gameObject.SetActive(false);
            spot.enabled = false;
        }
    }

    public void ChangePreview(int newPreview)
    {
        _nowPreview = newPreview;
        DisableSpots();
        if (_nowPreview != -1)
            _spotsPreviews[_nowPreview].gameObject.SetActive(true);
    }

    private void DisableSpots()
    {
        foreach (var spot in _spotsPreviews)
            spot.gameObject.SetActive(false);
    }

    public void Move(float offset)
    {
        transform.position += new Vector3(offset, 0, 0);
    }
}
