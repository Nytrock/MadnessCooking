using UnityEngine;

public class MenuManager : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameSaveManager _saveManager;
    [SerializeField] private ConfirmPanel _confirmPanel;
    [SerializeField] private SettingsManager _settings;
    [SerializeField] private TutorialManager _tutorial;
    [SerializeField] private MenuButtonSelector _buttonSelector;

    public void NewGameConfirm() {
        _buttonSelector.ChangeState(false);
        if (_saveManager.IsDataExists())
            _confirmPanel.StartConfirm(TutorialConfirm, "Menu.NewGameConfirm");
        else
            TutorialConfirm();
    }

    private void TutorialConfirm(bool isConfirm = true) {
        if (!isConfirm) {
            _buttonSelector.ChangeState(true);
            return;
        }

        _confirmPanel.StartConfirm(NewGame, "Menu.TutorialConfirmMessage", "Menu.TutorialConfirmTitle");
    }

    private void NewGame(bool isConfirm) {
        _saveManager.Delete();
        _tutorial.ChangeWorkState(isConfirm);
        _saveManager.Save();

        LoadGame();
    }

    public void LoadGame() {
        SceneUtility.LoadGame();
    }

    public void ChangeSettingsState() {
        ChangeState();
        _settings.ChangeState();
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void ExitToMenu() {
        SceneUtility.LoadMenu();
    }

    private void ChangeState() {
        Debug.Log(1);
        _panel.SetActive(!_panel.activeSelf);
        _buttonSelector.ChangeState(_panel.activeSelf);
    }

    public void ChangeState(bool newState) {
        Debug.Log(2);
        _panel.SetActive(newState);
        _buttonSelector.ChangeState(newState);
    }
}
