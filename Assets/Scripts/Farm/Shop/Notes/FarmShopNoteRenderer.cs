using UnityEngine;
using UnityEngine.UI;

public class FarmShopNoteRenderer : MonoBehaviour {
    [SerializeField] private Image _icon;
    [SerializeField] private LocalizedText _noteText;

    public void SetNote(FarmShopNote note) {
        _icon.sprite = note.Sprite;
        _icon.color += new Color(0, 0, 0, 1);
        _noteText.SetText(note.Note);
    }

    public void ResetNote() {
        _icon.color *= new Color(1, 1, 1, 0);
        _noteText.SetText("");
    }
}
