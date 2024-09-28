using UnityEngine;

[RequireComponent(typeof(ButtonWithAudio))]
public class MenuContinueButton : MonoBehaviour {
    [SerializeField] private GameSaveManager _saveManager;

    private void Start() {
        ButtonWithAudio button = GetComponent<ButtonWithAudio>();
        button.interactable = _saveManager.IsDataExists();
    }
}
