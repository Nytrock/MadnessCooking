using UnityEngine;
using UnityEngine.UI;

public class CafeSpot : MonoBehaviour {
    [SerializeField] private CafeSeat[] _seats;
    [SerializeField] private TableFoodView[] _tableFoods;
    [SerializeField] private GameObject _border;
    [SerializeField] private SpotRemoveButton _removeButton;
    private int _index;
    private bool _isEditor;

    public int Index => _index;
    public int SeatsCount => _seats.Length;
    public bool CanRemove => _removeButton.CanRemove;

    private void Start() {
        _border.SetActive(_isEditor);
        _removeButton.gameObject.SetActive(_isEditor);
    }

    public Vector2 GetTarget(int index) => _seats[index].transform.position;

    public Direction GetSeatRotation(int index) => _seats[index].SeatDirection;

    public void SetTableFoodSprite(Food food, int index) {
        _tableFoods[index].SetSprite(food.MiniSprite);
    }

    public void SetCameraManager(CameraManager cameraManager) {
        _removeButton.SetCameraManager(cameraManager);
    }

    public void ResetTableFoodSprite(int index) {
        _tableFoods[index].ResetSprite();
    }

    public void ChangeEditorState(bool state, bool isPreview = false) {
        _isEditor = state;
        _border.SetActive(_isEditor);
        _removeButton.gameObject.SetActive(_isEditor && !isPreview);
    }

    public void SetIndex(int index) {
        _index = index;
    }

    public void Destroy() {
        Destroy(gameObject);
    }

    public Button.ButtonClickedEvent GetOnClick() {
        return _removeButton.GetOnClick();
    }
}
