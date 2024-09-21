using UnityEngine;

public class UIStyleUpdater<TValue> : MonoBehaviour {
    [SerializeField] private ImageStyleChanger<TValue>[] _imageChangers;
    [SerializeField] private TextStyleChanger<TValue>[] _textChangers;

    public void UpdateStyle(TValue value) {
        foreach (var imageChanger in _imageChangers)
            imageChanger.UpdateStyle(value);

        foreach (var textChanger in _textChangers)
            textChanger.UpdateStyle(value);
    }
}
