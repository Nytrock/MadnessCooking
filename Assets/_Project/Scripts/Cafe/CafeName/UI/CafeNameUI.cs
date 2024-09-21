using System;
using TMPro;
using UnityEngine;

public class CafeNameUI : MonoBehaviour, ITutorialPart {
    [SerializeField] private CafeNameManager _manager;
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private CafeNameUIEditor _editor;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private string _localizationTable;
    [SerializeField] private string _defaultName;

    public event Action PartEnded;

    private void Awake() {
        _manager.NameChanged += UpdateName;
        _editor.NameEdited += SetNewName;
    }

    private void Start() {
        _editor.ChangeState(false);
    }

    private void UpdateName(string name) {
        if (name == string.Empty && !_tutorialManager.IsWork) {
            name = LocalizationManager.Instance.GetLocalization(_localizationTable, _defaultName);
            _editor.StartEditing(name, true);
        }

        _nameText.text = name;
    }

    public void EditCafeName() {
        _editor.StartEditing(_manager.CafeName);
    }

    private void SetNewName(string name) {
        _manager.ChangeName(name);
        PartEnded?.Invoke();
    }

    public void StartTutorialPart() {
        EditCafeName();
    }
}
