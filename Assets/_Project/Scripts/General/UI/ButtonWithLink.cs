using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonWithLink : MonoBehaviour {
    [SerializeField] private string _link;
    private Button _button;

    private void Awake() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OpenLink);
    }

    private void OpenLink() {
        Application.OpenURL(_link);
    }
}
