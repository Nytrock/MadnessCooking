using UnityEngine;
using UnityEngine.UI;

public class CafeSpot : MonoBehaviour {
    [SerializeField] private CafeSeat[] _seats;
    [SerializeField] private TableFoodView[] _tableFoods;
    [SerializeField] private GameObject _border;
    [SerializeField] private Button _removeButton;
    private int _index;
    private bool _isEditor;

    public int Index => _index;

    public int SeatsCount => _seats.Length;
    public Button RemoveButton => _removeButton;

    private void Start() {
        _border.SetActive(_isEditor);
        _removeButton.gameObject.SetActive(_isEditor);
    }

    public Vector2 GetTarget(int index) => _seats[index].transform.position;

    public Direction GetSeatRotation(int index) => _seats[index].SeatDirection;

    public void SetTableFoodSprite(Food food, int index) {
        _tableFoods[index].SetSprite(food.MiniSprite);
    }

    public void ResetTableFoodSprite(int index) {
        _tableFoods[index].ResetSprite();
    }

    public void ChangeEditorState(bool state) {
        _isEditor = state;
        _border.SetActive(_isEditor);
        _removeButton.gameObject.SetActive(_isEditor);
    }

    public void SetIndex(int index) {
        _index = index;
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}
