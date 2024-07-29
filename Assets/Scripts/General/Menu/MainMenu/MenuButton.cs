using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler {
    private Button _button;
    private RectTransform _rect;
    private int _index;

    public event Action<int> ButtonSelected;

    public int Index => _index;
    public RectTransform Rect => _rect;

    public void Awake() {
        _button = GetComponent<Button>();
        _rect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        ButtonSelected?.Invoke(_index);
    }

    public void SetInteractable(bool interactable) {
        _button.interactable = interactable;
    }

    public void SetIndex(int newIndex) {
        _index = newIndex;
    }

    public void Press() {
        _button.onClick.Invoke();
    }
}
