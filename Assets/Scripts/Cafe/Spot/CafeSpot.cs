using UnityEngine;
using UnityEngine.UI;

public class CafeSpot : MonoBehaviour
{
    [SerializeField] private CafeSeat [] _seats;
    [SerializeField] private TableFoodView [] _tableFoods;
    [SerializeField] private GameObject _outline;
    [SerializeField] private Button _removeButton;
    private int _index;
    private bool _isEditor;

    public int Index => _index;

    public int SeatsCount => _seats.Length;
    public Button RemoveButton => _removeButton;

    private void Start()
    {
        _outline.SetActive(_isEditor);
        _removeButton.gameObject.SetActive(_isEditor);
        for (int i = 0; i < _tableFoods.Length; i++)
            ResetTableFoodSprite(i);
    }

    public Transform GetTarget(int index) { return _seats[index].transform; }

    public float GetSeatRotation(int index)
    {
        return _seats[index].GetSeatRotation();
    }

    public void SetTableFoodSprite(Food food, int index)
    {
        _tableFoods[index].SetSprite(food.MiniSprite);
    }

    public void ResetTableFoodSprite(int index)
    {
        _tableFoods[index].ResetSprite();
    }

    public void ChangeEditorState(bool _state)
    {
        _isEditor = _state;
        _outline.SetActive(_isEditor);
        _removeButton.gameObject.SetActive(_isEditor);
    }

    public void SetIndex(int index)
    {
        _index = index;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
