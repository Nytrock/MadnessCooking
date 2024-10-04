using UnityEngine;

public class CafeSpot : MonoBehaviour {
    [SerializeField] private CafeSeat[] _seats;
    [SerializeField] private TableFoodRenderer[] _tableFoods;
    [SerializeField] private GameObject _border;
    [SerializeField] private SpotRemoveButton _removeButton;
    private int _index;
    private bool _isEditor;

    public int Index => _index;
    public int SeatsCount => _seats.Length;
    public ButtonWithAudio RemoveButton => _removeButton.Button;
    public bool CanRemove => _removeButton.CanRemove;

    private void Start() {
        _border.SetActive(_isEditor);
        _removeButton.gameObject.SetActive(_isEditor);
    }

    public Vector2 GetTarget(int index) => _seats[index].transform.position;

    public CafeSeat GetSeat(int index) => _seats[index];

    public void SetTableFoodSprite(Food food, int index) {
        _tableFoods[index].ShowFood(food);
    }

    public void SetCameraManager(CameraManager cameraManager) {
        _removeButton.SetCameraManager(cameraManager);
    }

    public void ResetTableFoodSprite(int index) {
        _tableFoods[index].HideFood();
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
}
