using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class ButtonOverRaycasts : MonoBehaviour {
    private Button _button;
    private Image _image;

    private void Awake() {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0))
            CheckMousePosition();
    }

    private void CheckMousePosition() {
        if (!_image.rectTransform.ContainsCamera())
            return;

        _button.onClick.Invoke();
        if (_button.TryGetComponent(out ButtonWithAudio audioButton))
            audioButton.ForceSoundPlay();
    }
}
