using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuContinueButton : MonoBehaviour {
    [SerializeField] private GameSaveManager _saveManager;
    private void Start() {
        Button button = GetComponent<Button>();
        button.interactable = _saveManager.IsDataExists();
    }
}
