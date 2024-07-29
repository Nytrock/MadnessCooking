using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameSaveManager _saveManager;
    [SerializeField] private MenuButton _continueButton;
    [SerializeField] private ConfirmPanel _confirmPanel;
    [SerializeField] private SettingsManager _settings;
    [SerializeField] private MenuButtonSelector _buttonSelector;

    private void Start() {
        _continueButton.SetInteractable(_saveManager.IsDataExists());
        ChangeState(true);
    }

    public void NewGameConfirm() {
        _buttonSelector.ChangeState(false);
        if (_saveManager.IsDataExists())
            _confirmPanel.StartConfirm(NewGame, "Menu.NewGameConfirm");
        else
            NewGame();
    }

    private void NewGame(bool isConfirm = true) {
        if (!isConfirm) {
            _buttonSelector.ChangeState(true);
            return;
        }

        _saveManager.Delete();
        LoadGame();
    }

    public void LoadGame() {
        SceneManager.LoadScene(1);
    }

    public void ChangeSettingsState() {
        ChangeState();
        _settings.ChangeState();
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void ExitMenu() {
        SceneManager.LoadScene(0);
    }

    private void ChangeState() {
        _panel.SetActive(!_panel.activeSelf);
        _buttonSelector.ChangeState(_panel.activeSelf);
    }

    private void ChangeState(bool newState) {
        _panel.SetActive(newState);
        _buttonSelector.ChangeState(newState);
    }
}
