using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ButtonWithAudio))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler {
    private ButtonWithAudio _button;
    private RectTransform _rect;
    private int _index;

    public event Action<int> ButtonSelected;

    public int Index => _index;
    public RectTransform Rect => _rect;

    public void Awake() {
        _button = GetComponent<ButtonWithAudio>();
        _rect = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        ButtonSelected?.Invoke(_index);
    }

    public void SetIndex(int newIndex) {
        _index = newIndex;
    }

    public void Press() {
        _button.onClick.Invoke();
    }
}
