using UnityEngine;

public class MenuManager : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameSaveManager _saveManager;
    [SerializeField] private ScenesManager _sceneManager;
    [SerializeField] private ConfirmPanel _confirmPanel;
    [SerializeField] private SettingsManager _settings;
    [SerializeField] private TutorialManager _tutorial;

    private void Awake() {
        _settings.SettingsClosed += ChangeSettingsState;
    }

    public void NewGameConfirm() {
        if (_saveManager.IsDataExists())
            _confirmPanel.StartConfirm(TutorialConfirm, "Menu.NewGameConfirm");
        else
            TutorialConfirm();
    }

    private void TutorialConfirm(bool isConfirm = true) {
        if (!isConfirm)
            return;

        _confirmPanel.StartConfirm(NewGame, "Menu.TutorialConfirmMessage", "Menu.TutorialConfirmTitle");
    }

    private void NewGame(bool isConfirm) {
        _saveManager.Delete();
        _tutorial.ChangeWorkState(isConfirm);
        _saveManager.Save();

        LoadGame();
    }

    public void LoadGame() {
        _sceneManager.LoadGame();
    }

    public void ChangeSettingsState() {
        ChangeState();
        _settings.ChangeState();
    }

    public void ExitGame() {
        _saveManager.Save();
        Application.Quit();
    }

    public void ExitToMenu() {
        _saveManager.Save();
        _sceneManager.LoadMenu();
    }

    private void ChangeState() {
        _panel.SetActive(!_panel.activeSelf);
    }
}
