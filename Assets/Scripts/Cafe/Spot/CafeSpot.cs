using UnityEngine;
using UnityEngine.UI;

public class CafeSpot : MonoBehaviour
{
    [SerializeField] private CafeSeat [] _seats;
    [SerializeField] private SpriteRenderer [] _tableFoods;
    [SerializeField] private SpriteRenderer _outline;
    [SerializeField] private Button _removeButton;
    private int _index;
    private bool _isEditor;

    public int Index => _index;

    public int SeatsCount => _seats.Length;
    public Button RemoveButton => _removeButton;

    private void Start()
    {
        _outline.gameObject.SetActive(_isEditor);
        _removeButton.gameObject.SetActive(_isEditor);
    }

    public Transform GetTarget(int index) { return _seats[index].transform; }

    public float GetSpotSize() { return transform.localScale.x; }

    public void SetTableFoodSprite(Food food, int index)
    {
        _tableFoods[index].sprite = food.MiniSprite;
    }

    public void ResetTableFoodSprite(int index)
    {
        _tableFoods[index].sprite = null;
    }

    public void ChangeEditorState(bool _state)
    {
        _isEditor = _state;
        _outline.gameObject.SetActive(_isEditor);
        _removeButton.gameObject.SetActive(_isEditor);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
