using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using NodeDirection = UnityEditor.Experimental.GraphView.Direction;

public abstract class DialogueBaseNode : Node {
    private string _id;
    private string _dialogueName;
    protected List<DialogueChoiceSaveData> _choices;
    private string _text;

    protected abstract DialogueType _type { get; }

    private Color _defaultBackgroundColor;
    protected DialogueSystemGraphView _graphView;
    private DialogueSystemGroup _group;

    public string DialogueName => _dialogueName;
    public Group Group => _group;
    public string ID => _id;

    public virtual void Initialize(DialogueSystemGraphView graphView, Vector2 position) {
        _id = Guid.NewGuid().ToString();
        _dialogueName = "DialogueName";
        _choices = new();
        _text = "Dialogue text.";
        _defaultBackgroundColor = new(29f / 255f, 29f / 255f, 30f / 255f);
        _graphView = graphView;

        SetPosition(new(position, Vector2.zero));

        mainContainer.AddToClassList("ds-node__main-container");
        extensionContainer.AddToClassList("ds-node__extension-container");
    }

    #region Overrided Methods
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt) {
        evt.menu.AppendAction("Disconnect Input Ports", actionEvent => DisconnectInputPorts());
        evt.menu.AppendAction("Disconnect Output Ports", actionEvent => DisconnectOutputPorts());
        base.BuildContextualMenu(evt);
    }
    #endregion

    public virtual void Draw() {
        TextField dialogueNameField = UIElementUtility.CreateTextField(_dialogueName, onValueChanged: callback => {
            TextField target = callback.target as TextField;
            target.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();

            if (_group == null) {
                _graphView.RemoveUngroupedNode(this);
                _dialogueName = target.value;
                _graphView.AddUngroupedNode(this);
                return;
            }

            DialogueSystemGroup currentGroup = _group;
            _graphView.RemoveGroupedNode(this, _group);
            _dialogueName = target.value;
            _graphView.AddGroupedNode(this, currentGroup);
        });
        dialogueNameField.AddClasses(
            "ds-node__text-field",
            "ds-node__text-field__hidden",
            "ds-node__filename-text-field"
        );
        titleContainer.Insert(0, dialogueNameField);

        Port inputPort = this.CreatePort("Dialogue Connection", direction: NodeDirection.Input, capacity: Port.Capacity.Multi);
        inputContainer.Add(inputPort);

        Foldout textFoldout = UIElementUtility.CreateFoldout("Dialogue Text");
        TextField dialogueTextField = UIElementUtility.CreateTextArea(_text);
        dialogueTextField.AddClasses(
            "ds-node__text-field",
            "ds-node__quote-text-field"
        );
        textFoldout.Add(dialogueTextField);

        VisualElement customDataContainer = new();
        customDataContainer.AddToClassList("ds-node__custom-data-container");
        customDataContainer.Add(textFoldout);
        extensionContainer.Add(customDataContainer);

        foreach (var choice in _choices) {
            Port choicePort = CreateChoicePort(choice);
            choicePort.userData = choice;
            outputContainer.Add(choicePort);
        }

        RefreshExpandedState();
    }

    public void ChangeGroup(DialogueSystemGroup group) {
        _group = group;
    }

    #region Utility
    public void DisconnectAllPorts() {
        DisconnectInputPorts();
        DisconnectOutputPorts();
    }

    private void DisconnectInputPorts() {
        DisconnectPorts(inputContainer);
    }

    private void DisconnectOutputPorts() {
        DisconnectPorts(outputContainer);
    }

    private void DisconnectPorts(VisualElement container) {
        foreach (var element in container.Children())
            if (element is Port port)
                if (port.connected)
                    _graphView.DeleteElements(port.connections);
    }

    public void SetErrorStyle(Color color) {
        mainContainer.style.backgroundColor = color;
    }

    public void ResetStyle() {
        mainContainer.style.backgroundColor = _defaultBackgroundColor;
    }
    #endregion

    protected abstract Port CreateChoicePort(object userData);
}
