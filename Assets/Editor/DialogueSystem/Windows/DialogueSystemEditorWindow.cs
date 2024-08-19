using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class DialogueSystemEditorWindow : EditorWindow {
    private readonly string _defaultFileName = "DialogueName";
    private TextField _fileNameField;
    private Button _saveButton;

    [MenuItem("Window/Dialogue System/Dialogue Graph")]
    public static void ShowExample() {
        GetWindow<DialogueSystemEditorWindow>("Dialogue Graph");
    }

    private void CreateGUI() {
        AddGraphView();
        AddToolbar();

        AddStyles();
    }

    #region Addition
    private void AddGraphView() {
        DialogueSystemGraphView graphView = new(this);
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }

    private void AddToolbar() {
        Toolbar toolbar = new();
        toolbar.AddStyleSheets("DialogueSystem/ToolbarStyles.uss");

        _fileNameField = UIElementUtility.CreateTextField(_defaultFileName, "File Name: ", callback => {
            _fileNameField.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();
        });
        toolbar.Add(_fileNameField);

        _saveButton = UIElementUtility.CreateButton("Save");
        toolbar.Add(_saveButton);

        rootVisualElement.Add(toolbar);
    }

    private void AddStyles() {
        rootVisualElement.AddStyleSheets("DialogueSystem/Variables.uss");
    }
    #endregion

    public void ChangeSaveButtonState(bool newStae) {
        _saveButton.SetEnabled(newStae);
    }
}
