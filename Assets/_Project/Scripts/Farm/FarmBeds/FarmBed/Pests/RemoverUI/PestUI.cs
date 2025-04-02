using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PestUI : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler {
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _selection;
    [SerializeField] private Image[] _selectionImages;

    private PestsRemoverUI _pestsRemover;
    private Button _button;

    public Pest Pest { get; private set; }

    private void Awake() {
        _button = GetComponent<Button>();
    }

    public void Setup(Pest pest, RangeVector position, PestsRemoverUI remover) {
        _pestsRemover = remover;

        _image.sprite = pest.GetSprite();
        _image.SetNativeSize();
        foreach (var selectionImage in _selectionImages) {
            selectionImage.sprite = _image.sprite;
            selectionImage.SetNativeSize();
        }

        Vector2 normalizedPosition = pest.Data.NormalizedPosition;
        transform.SetPositionAndRotation(position.Lerp(normalizedPosition), pest.Data.RotationDegree);

        Pest = pest;
    }

    public void ResetPest() {
        ChangeState(false);
        _button.onClick.RemoveAllListeners();
    }

    public void ChangeState(bool newState) {
        gameObject.SetActive(newState);
        ChangeSelectionState(false);
    }

    public void SetupRemoveButton() {
        _button.onClick.AddListener(delegate { _pestsRemover.RemovePest(this); });
    }

    public void OnPointerExit(PointerEventData eventData) {
        ChangeSelectionState(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        ChangeSelectionState(true);
    }

    private void ChangeSelectionState(bool isSelected) {
        _selection.SetActive(isSelected);
    }
}
