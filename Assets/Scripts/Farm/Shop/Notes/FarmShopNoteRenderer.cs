using UnityEngine;

public class FarmShopNoteRenderer : MonoBehaviour {
    [SerializeField] private LocalizedText _noteText;

    public void SetNote(FarmShopNote note) {
        _noteText.SetText(note.Note);
    }

    public void ResetNote() {
        _noteText.SetText("");
    }
}
