using UnityEngine;
using UnityEngine.Events;

public class CafeSpot : MonoBehaviour {
    [SerializeField] private CafeSeat[] _seats;
    [SerializeField] private GameObject _border;
    [SerializeField] private SpotRemoveButton _removeButton;

    private bool _isEditor;

    public int SeatsCount => _seats.Length;

    private void Start() {
        _border.SetActive(_isEditor);
        _removeButton.gameObject.SetActive(_isEditor);
    }

    public CafeSeat GetSeat(int index) => _seats[index];

    public void ChangeEditorState(bool state, bool isPreview = false) {
        _isEditor = state;
        _border.SetActive(_isEditor);
        _removeButton.gameObject.SetActive(_isEditor && !isPreview);
    }

    public void SetupRemoveButton(UnityAction buttonAction, AudioSource buttonAudio) {
        _removeButton.Setup(buttonAction, buttonAudio);
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}
