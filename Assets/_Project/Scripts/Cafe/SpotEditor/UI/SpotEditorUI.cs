using UnityEngine;

public class SpotEditorUI : MonoBehaviour {
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private GameObject _buttonAdd;
    [SerializeField] private SpotEditorChooseButtons _buttonsContainer;
    [SerializeField] private SpotEditor _editor;
    private float _cellSize = -1;

    private void Awake() {
        _editor.EditorActivated += delegate { ChangeCanvasState(true); SetCanvasPosition(_editor.SpotManager.GetLengthOfAllSpots()); };
        _editor.EditorDisabled += delegate { ChangeCanvasState(false); SetCanvasPosition(0); };
        _editor.SpotManager.SpotsPositionChanged += MoveCanvas;
    }

    private void Start() {
        _cellSize = _editor.SpotManager.CellSize;
        ChangeCanvasState(false);
        ChangeChoiceState(false);
    }

    public void ChangeCanvasState(bool newState) {
        _canvas.gameObject.SetActive(newState);

        if (newState) {
            ChangeChoiceState(false);
        } else {
            ResizeCanvas(0);
            SetCanvasPosition(0);
        }
    }

    private void MoveCanvas(float offset) {
        _canvas.position += new Vector3(offset, 0, 0);
    }

    private void SetCanvasPosition(float xPosition) {
        _canvas.position = new Vector3(xPosition - (_cellSize / 2), _canvas.position.y, _canvas.position.z);
    }

    public void ChangeChoiceState(bool newState) {
        _buttonsContainer.gameObject.SetActive(newState);
        _buttonAdd.SetActive(!newState);

        if (newState) {
            _buttonsContainer.SetButtonsNumber(_editor.SpotManager.GetFreeSpace());
            _editor.SetPreviewIndex(0);
        }
    }

    public void SetPreview(int previewIndex) {
        _editor.SetPreviewIndex(previewIndex);
        ResizeCanvas(Mathf.Max(previewIndex, 0));
    }

    private void ResizeCanvas(int previewIndex) {
        float canvasNewWidth = (previewIndex + 1) * _cellSize;
        float canvasOldWidth = _canvas.sizeDelta.x;
        _canvas.sizeDelta = new Vector2(canvasNewWidth, _canvas.sizeDelta.y);
        MoveCanvas((canvasNewWidth - canvasOldWidth) / 2);
    }

    public void AddSpot() {
        ChangeChoiceState(false);
        _editor.AddNewSpot();
        ResizeCanvas(0);
    }
}
