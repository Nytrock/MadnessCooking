using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueSystemGraphView : GraphView {
    private readonly DialogueSystemEditorWindow _editorWindow;
    private DialogueSystemSearhWindow _searhWindow;

    private readonly SerializableDictionary<string, DialogueNodeErrorData> _ungroupedNodes;
    private readonly SerializableDictionary<Group, SerializableDictionary<string, DialogueNodeErrorData>> _groupedNodes;
    private readonly SerializableDictionary<string, DialogueGroupErrorData> _groups;

    private int _repeatedNamesCount;

    public DialogueSystemGraphView(DialogueSystemEditorWindow editor) {
        _editorWindow = editor;
        _ungroupedNodes = new();
        _groupedNodes = new();
        _groups = new();

        AddSearchWindow();
        AddManipulators();
        AddGridBackground();
        AddStyle();

        OnElementsDeleted();
        OnGroupElementAdded();
        OnGroupElementRemoved();
        OnGroupRenamed();
        OnGraphViewChanged();
    }

    #region Overrided Methods
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) {
        List<Port> compatiblePorts = new();
        ports.ForEach(port => {
            if (startPort == port)
                return;

            if (startPort.node == port.node)
                return;

            if (startPort.direction == port.direction)
                return;

            compatiblePorts.Add(port);
        });
        return compatiblePorts;
    }
    #endregion

    #region Manipulators
    private void AddManipulators() {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        this.AddManipulator(CreateNodeContextualMenu("Add Node (Single Choice)", DialogueType.SingleChoice));
        this.AddManipulator(CreateNodeContextualMenu("Add Node (Multiple Choice)", DialogueType.MultipleChoice));
        this.AddManipulator(CreateGroupContextualMenu());
    }

    private IManipulator CreateNodeContextualMenu(string actionName, DialogueType type) {
        ContextualMenuManipulator contextualManipulator = new(
            menuEvent => menuEvent.menu.AppendAction(actionName, actionEvent => CreateNode(type, GetLocalMousePosition(actionEvent.eventInfo.localMousePosition)))
        );
        return contextualManipulator;
    }

    private IManipulator CreateGroupContextualMenu() {
        ContextualMenuManipulator contextualManipulator = new(
            menuEvent => menuEvent.menu.AppendAction("Add Group", actionEvent => CreateGroup(GetLocalMousePosition(actionEvent.eventInfo.localMousePosition)))
        );
        return contextualManipulator;
    }
    #endregion

    #region Stylizing
    private void AddStyle() {
        this.AddStyleSheets(
            "DialogueSystem/GraphViewStyles.uss",
            "DialogueSystem/NodeStyles.uss"
        );
    }

    private void AddGridBackground() {
        GridBackground gridBackground = new();
        gridBackground.StretchToParentSize();
        Insert(0, gridBackground);
    }
    #endregion

    #region Callbacks
    private void OnElementsDeleted() {
        deleteSelection = (operationName, askUser) => {
            List<DialogueBaseNode> nodesToDelete = new();
            List<DialogueSystemGroup> groupsToDelete = new();
            List<Edge> edgesToDelete = new();

            foreach (var element in selection) {
                if (element is DialogueBaseNode node)
                    nodesToDelete.Add(node);

                if (element is DialogueSystemGroup group)
                    groupsToDelete.Add(group);

                if (element is Edge edge)
                    edgesToDelete.Add(edge);
            }

            foreach (var group in groupsToDelete) {
                List<DialogueBaseNode> groupNodes = new();
                foreach (var groupElement in group.containedElements)
                    if (groupElement is DialogueBaseNode node)
                        groupNodes.Add(node);

                group.RemoveElements(groupNodes);
                RemoveGroup(group);
                RemoveElement(group);
            }

            DeleteElements(edgesToDelete);

            foreach (var node in nodesToDelete) {
                node.Group?.RemoveElement(node);
                RemoveUngroupedNode(node);
                node.DisconnectAllPorts();
                RemoveElement(node);
            }

        };
    }

    private void OnGroupElementAdded() {
        elementsAddedToGroup = (group, elements) => {
            foreach (var element in elements) {
                if (element is not DialogueBaseNode)
                    continue;

                DialogueBaseNode node = (DialogueBaseNode)element;
                RemoveUngroupedNode(node);
                AddGroupedNode(node, group as DialogueSystemGroup);
            }
        };
    }

    private void OnGroupElementRemoved() {
        elementsRemovedFromGroup = (group, elements) => {
            foreach (var element in elements) {
                if (element is not DialogueBaseNode)
                    continue;

                DialogueBaseNode node = (DialogueBaseNode)element;
                RemoveGroupedNode(node, group);
                AddUngroupedNode(node);
            }
        };
    }

    private void OnGroupRenamed() {
        groupTitleChanged = (group, newTitle) => {
            DialogueSystemGroup dialogueGroup = (DialogueSystemGroup)group;
            dialogueGroup.title = newTitle.RemoveWhitespaces().RemoveSpecialCharacters();

            RemoveGroup(dialogueGroup);
            dialogueGroup.UpdateTitle();
            AddGroup(dialogueGroup);
        };
    }

    private void OnGraphViewChanged() {
        graphViewChanged = (changes) => {
            foreach (var edge in changes.edgesToCreate) {
                DialogueBaseNode nextNode = edge.input.node as DialogueBaseNode;
                DialogueChoiceSaveData choiceData = edge.output.userData as DialogueChoiceSaveData;
                choiceData.SetNode(nextNode);
            }

            foreach (var element in changes.elementsToRemove) {
                if (element is Edge edge) {
                    DialogueChoiceSaveData choiceData = edge.output.userData as DialogueChoiceSaveData;
                    choiceData.ResetNode();
                }
            }

            return changes;
        };
    }
    #endregion

    #region ElementsCreation
    public void CreateNode(DialogueType type, Vector2 globalPosition) {
        DialogueBaseNode node;
        if (type == DialogueType.SingleChoice)
            node = new DialogueSingleChoiceNode();
        else
            node = new DialogueMultipleChoiceNode();

        node.Initialize(this, globalPosition);
        node.Draw();

        AddUngroupedNode(node);
        AddElement(node);
    }

    public void CreateGroup(Vector2 position) {
        DialogueSystemGroup group = new("Dialogue group", position);
        AddGroup(group);
        AddElement(group);

        foreach (var element in selection)
            if (element is DialogueBaseNode node)
                group.AddElement(node);
    }

    private void AddSearchWindow() {
        if (_searhWindow != null)
            return;

        _searhWindow = ScriptableObject.CreateInstance<DialogueSystemSearhWindow>();
        _searhWindow.Initialize(this);


        nodeCreationRequest = context => SearchWindow.Open(
            new SearchWindowContext(context.screenMousePosition),
            _searhWindow
        );
    }
    #endregion

    #region Utilities
    public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearhWindow = false) {
        Vector2 worldMousePosition = mousePosition;
        if (isSearhWindow)
            worldMousePosition -= _editorWindow.position.position;

        Vector2 localMousePosition = contentViewContainer.WorldToLocal(worldMousePosition);
        return localMousePosition;
    }
    #endregion

    #region Repeated Elements
    public void AddUngroupedNode(DialogueBaseNode node) {
        string nodeName = node.DialogueName.ToLower();
        if (!_ungroupedNodes.ContainsKey(nodeName))
            _ungroupedNodes[nodeName] = new();

        _ungroupedNodes[nodeName].AddNode(node);
        if (_ungroupedNodes[nodeName].IsError)
            ChangeRepeatedNamesCount(1);
    }

    public void RemoveUngroupedNode(DialogueBaseNode node) {
        string nodeName = node.DialogueName.ToLower();
        _ungroupedNodes[nodeName].RemoveNode(node);

        if (_ungroupedNodes[nodeName].IsEmpty())
            _ungroupedNodes.Remove(nodeName);
        else
            ChangeRepeatedNamesCount(-1);
    }

    public void AddGroupedNode(DialogueBaseNode node, DialogueSystemGroup group) {
        string nodeName = node.DialogueName.ToLower();
        node.ChangeGroup(group);

        if (!_groupedNodes.ContainsKey(group))
            _groupedNodes[group] = new();

        if (!_groupedNodes[group].ContainsKey(nodeName))
            _groupedNodes[group][nodeName] = new();

        _groupedNodes[group][nodeName].AddNode(node);
        if (_groupedNodes[group][nodeName].IsError)
            ChangeRepeatedNamesCount(1);
    }

    public void RemoveGroupedNode(DialogueBaseNode node, Group group) {
        string nodeName = node.DialogueName.ToLower();
        node.ChangeGroup(null);
        _groupedNodes[group][nodeName].RemoveNode(node);

        if (_groupedNodes[group][nodeName].IsEmpty())
            _groupedNodes[group].Remove(nodeName);
        else
            ChangeRepeatedNamesCount(-1);

        if (_groupedNodes[group].Count == 0)
            _groupedNodes.Remove(group);
    }

    private void AddGroup(DialogueSystemGroup group) {
        string groupName = group.OldTitle.ToLower();
        if (!_groups.ContainsKey(groupName))
            _groups[groupName] = new();

        _groups[groupName].AddGroup(group);
        if (_groups[groupName].IsError)
            ChangeRepeatedNamesCount(1);
    }

    public void RemoveGroup(DialogueSystemGroup group) {
        string groupName = group.OldTitle.ToLower();
        _groups[groupName].RemoveGroup(group);

        if (_groups[groupName].IsEmpty())
            _groups.Remove(groupName);
        else
            ChangeRepeatedNamesCount(-1);
    }

    private void ChangeRepeatedNamesCount(int value) {
        _repeatedNamesCount += value;
        _editorWindow.ChangeSaveButtonState(_repeatedNamesCount == 0);
    }
    #endregion
}
