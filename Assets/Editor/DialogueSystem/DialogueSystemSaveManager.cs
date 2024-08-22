using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class DialogueSystemSaveManager {
    private static DialogueSystemGraphView _graphView;

    private static string _graphFileName;
    private static string _graphFolderPath;

    private static List<DialogueSystemGroup> _groups;
    private static List<DialogueBaseNode> _nodes;
    private static Dictionary<string, DialogueGroup> _createdDialogueGroups;
    private static Dictionary<string, Dialogue> _createdDialogues;

    private static Dictionary<string, DialogueSystemGroup> _loadedGroups;
    private static Dictionary<string, DialogueBaseNode> _loadedNodes;

    public static void Initialize(DialogueSystemGraphView graphView, string graphName) {
        _graphView = graphView;
        _graphFileName = graphName;
        _graphFolderPath = $"Assets/ScriptableObjects/Dialogues/{graphName}";

        _groups = new();
        _nodes = new();
        _createdDialogueGroups = new();
        _createdDialogues = new();
        _loadedGroups = new();
        _loadedNodes = new();
    }

    #region Save
    public static void Save() {
        CreateStaticFolders();
        GetElementsFromGraphView();

        DialogueSystemGraphSaveData graphData = CreateAsset<DialogueSystemGraphSaveData>("Assets/Editor/DialogueSystem/Graphs", _graphFileName);
        graphData.Initialize(_graphFileName);

        DialogueContainer dialogueContainer = CreateAsset<DialogueContainer>(_graphFolderPath, _graphFileName);
        dialogueContainer.Initialize(_graphFileName);

        SaveGroups(graphData, dialogueContainer);
        SaveNodes(graphData, dialogueContainer);

        SaveAsset(graphData);
        SaveAsset(dialogueContainer);
    }

    #region Groups
    private static void SaveGroups(DialogueSystemGraphSaveData graphData, DialogueContainer dialogueContainer) {
        List<string> groupNames = new();
        foreach (var group in _groups) {
            SaveGroupToGraph(group, graphData);
            SaveGroupToScriptableObject(group, dialogueContainer);
            groupNames.Add(group.title);
        }

        UpdateOldGroups(groupNames, graphData);
    }

    private static void SaveGroupToGraph(DialogueSystemGroup group, DialogueSystemGraphSaveData graphData) {
        DialogueGroupSaveData groupData = new(
            group.ID,
            group.title,
            group.GetPosition().position
        );

        graphData.AddGroup(groupData);
    }

    private static void SaveGroupToScriptableObject(DialogueSystemGroup group, DialogueContainer dialogueContainer) {
        string groupName = group.title;
        CreateFolder($"{_graphFolderPath}/Groups", groupName);
        CreateFolder($"{_graphFolderPath}/Groups/{groupName}", "Dialogues");

        DialogueGroup dialogueGroup = CreateAsset<DialogueGroup>($"{_graphFolderPath}/Groups/{groupName}", groupName);
        dialogueGroup.Initialize(groupName);
        dialogueContainer.AddGroup(dialogueGroup);
        _createdDialogueGroups.Add(group.ID, dialogueGroup);

        SaveAsset(dialogueGroup);
    }

    private static void UpdateOldGroups(List<string> currentGroupNames, DialogueSystemGraphSaveData graphData) {
        foreach (var oldName in graphData.OldGroupNames) {
            if (currentGroupNames.Contains(oldName))
                continue;

            RemoveFolder($"{_graphFolderPath}/Groups/{oldName}");
        }

        graphData.UpdateOldGroupNames(new(currentGroupNames));
    }
    #endregion

    #region Nodes
    private static void SaveNodes(DialogueSystemGraphSaveData graphData, DialogueContainer dialogueContainer) {
        List<string> nodeNames = new();
        SerializableDictionary<string, List<string>> groupedNodeNames = new();

        foreach (var node in _nodes) {
            SaveNodeToGraph(node, graphData);
            SaveNodeToScriptableObject(node, dialogueContainer);
            if (node.Group != null) {
                if (groupedNodeNames.ContainsKey(node.Group.ID))
                    groupedNodeNames[node.Group.ID].Add(node.DialogueName);
                else
                    groupedNodeNames[node.Group.ID] = new() { node.DialogueName };
                continue;
            }

            nodeNames.Add(node.DialogueName);
        }

        UpdateDialoguesChoicesConnections();
        UpdateOldGroupedNodes(groupedNodeNames, graphData);
        UpdateOldUngroupedNodes(nodeNames, graphData);
    }

    private static void SaveNodeToGraph(DialogueBaseNode node, DialogueSystemGraphSaveData graphData) {
        List<DialogueChoiceSaveData> choices = CloneNodeChoices(node.Choices);

        DialogueNodeSaveData nodeData = new(
            node.ID,
            node.DialogueName,
            node.Text,
            choices,
            node.Group?.ID,
            node.DialogueType,
            node.GetPosition().position
        );

        graphData.AddNode(nodeData);
    }

    private static List<DialogueChoiceSaveData> CloneNodeChoices(IEnumerable<DialogueChoiceSaveData> originalChoices) {
        List<DialogueChoiceSaveData> choices = new();
        foreach (var choice in originalChoices)
            choices.Add(choice.Copy());
        return choices;
    }

    private static void SaveNodeToScriptableObject(DialogueBaseNode node, DialogueContainer dialogueContainer) {
        Dialogue dialogue;
        if (node.Group != null) {
            dialogue = CreateAsset<Dialogue>($"{_graphFolderPath}/Groups/{node.Group.title}/Dialogues", node.DialogueName);
            dialogueContainer.AddGroupDialogue(_createdDialogueGroups[node.Group.ID], dialogue);
        } else {
            dialogue = CreateAsset<Dialogue>($"{_graphFolderPath}/Global/Dialogues", node.DialogueName);
            dialogueContainer.AddUngroupDialogue(dialogue);
        }

        dialogue.Initialize(
            node.DialogueName,
            node.Text,
            ConvertNodeChoicesToDialogueChoices(node.Choices),
            node.DialogueType,
            node.IsStartingNode()
        );
        _createdDialogues.Add(node.ID, dialogue);

        SaveAsset(dialogue);
    }

    private static void UpdateDialoguesChoicesConnections() {
        foreach (var node in _nodes) {
            Dialogue dialogue = _createdDialogues[node.ID];

            for (int i = 0; i < node.Choices.Count(); i++) {
                DialogueChoiceSaveData nodeChoice = node.GetChoice(i);
                if (string.IsNullOrEmpty(nodeChoice.NodeID))
                    continue;

                dialogue.SetChoiceNextDialogue(_createdDialogues[nodeChoice.NodeID], i);
                SaveAsset(dialogue);
            }
        }
    }

    private static List<DialogueChoiceData> ConvertNodeChoicesToDialogueChoices(IEnumerable<DialogueChoiceSaveData> nodeChoices) {
        List<DialogueChoiceData> dialogueChoices = new();
        foreach (var nodeChoice in nodeChoices)
            dialogueChoices.Add(nodeChoice.ToDialogueChoice());
        return dialogueChoices;
    }

    private static void UpdateOldUngroupedNodes(List<string> currentNodeNames, DialogueSystemGraphSaveData graphData) {
        foreach (var oldName in graphData.OldUngroupedNodeNames) {
            if (currentNodeNames.Contains(oldName))
                continue;

            RemoveAsset($"{_graphFolderPath}/Global/Dialogues", oldName);
        }

        graphData.UpdateOldUngroupedNodeNames(new(currentNodeNames));
    }

    private static void UpdateOldGroupedNodes(SerializableDictionary<string, List<string>> currentGroupedNodeNames, DialogueSystemGraphSaveData graphData) {
        foreach (var oldGroupedNode in graphData.OldGroupedNodeNames) {
            if (!currentGroupedNodeNames.ContainsKey(oldGroupedNode.Key))
                continue;

            foreach (var groupedNode in oldGroupedNode.Value) {
                if (currentGroupedNodeNames[oldGroupedNode.Key].Contains(groupedNode))
                    continue;

                RemoveAsset($"{_graphFolderPath}/Global/{oldGroupedNode.Key}/Dialogues", groupedNode);
            }
        }

        graphData.UpdateOldGroupedNodeNames(new(currentGroupedNodeNames));
    }
    #endregion

    private static void GetElementsFromGraphView() {
        _graphView.graphElements.ForEach(graphElement => {
            if (graphElement is DialogueBaseNode node)
                _nodes.Add(node);
            else if (graphElement is DialogueSystemGroup group)
                _groups.Add(group);
        });
    }
    #endregion

    #region Load
    public static void Load() {
        DialogueSystemGraphSaveData graphData = LoadAsset<DialogueSystemGraphSaveData>("Assets/Editor/DialogueSystem/Graphs", _graphFileName);
        if (graphData == null) {
            EditorUtility.DisplayDialog(
                "Cannot load the file!",
                $"File {_graphFileName}.asset cannot be found. Change name and try again.",
                "Ok"
            );
            return;
        }

        DialogueSystemEditorWindow.UpdateFileName(graphData.FileName);

        LoadGroups(graphData);
        LoadNodes(graphData);
        LoadNodesConnections();
    }

    private static void LoadGroups(DialogueSystemGraphSaveData graphData) {
        foreach (var groupData in graphData.Groups) {
            DialogueSystemGroup group = _graphView.CreateGroup(groupData.Name, groupData.Position);
            group.SetID(groupData.ID);
            _loadedGroups.Add(groupData.ID, group);
        }
    }

    private static void LoadNodes(DialogueSystemGraphSaveData graphData) {
        foreach (var nodeData in graphData.Nodes) {
            List<DialogueChoiceSaveData> choices = CloneNodeChoices(nodeData.Choices);
            DialogueBaseNode node = _graphView.CreateNode(nodeData.Name, nodeData.DialogueType, nodeData.Position, false);
            node.Setup(nodeData, choices);
            node.Draw();

            _loadedNodes.Add(node.ID, node);

            if (string.IsNullOrEmpty(nodeData.GroupID))
                continue;

            DialogueSystemGroup group = _loadedGroups[nodeData.GroupID];
            node.ChangeGroup(group);
            group.AddElement(node);
        }
    }

    private static void LoadNodesConnections() {
        foreach (var loadedNode in _loadedNodes) {
            foreach (var element in loadedNode.Value.outputContainer.Children()) {
                if (element is not Port choicePort)
                    continue;

                DialogueChoiceSaveData choiceData = (DialogueChoiceSaveData)choicePort.userData;
                if (string.IsNullOrEmpty(choiceData.NodeID))
                    continue;

                DialogueBaseNode nextNode = _loadedNodes[choiceData.NodeID];
                Port nextNodeInputPort = (Port)nextNode.inputContainer.Children().First();
                _graphView.AddElement(choicePort.ConnectTo(nextNodeInputPort));
            }
            loadedNode.Value.RefreshPorts();
        }
    }
    #endregion

    #region Folders
    private static void CreateStaticFolders() {
        CreateFolder("Assets/Editor/DialogueSystem", "Graphs");
        CreateFolder("Assets/ScriptableObjects", "Dialogues"); ;
        CreateFolder("Assets/ScriptableObjects/Dialogues", _graphFileName); ;

        CreateFolder(_graphFolderPath, "Global");
        CreateFolder(_graphFolderPath, "Groups");
        CreateFolder($"{_graphFolderPath}/Global", "Dialogues");
    }

    public static void CreateFolder(string path, string folderName) {
        if (AssetDatabase.IsValidFolder($"{path}/{folderName}"))
            return;

        AssetDatabase.CreateFolder(path, folderName);
    }

    private static void RemoveFolder(string path) {
        FileUtil.DeleteFileOrDirectory($"{path}.meta");
        FileUtil.DeleteFileOrDirectory($"{path}/");
    }

    #endregion

    #region Assets
    public static TAsset CreateAsset<TAsset>(string path, string assetName) where TAsset : ScriptableObject {
        TAsset asset = LoadAsset<TAsset>(path, assetName);
        if (asset != null)
            return asset;

        asset = ScriptableObject.CreateInstance<TAsset>();
        AssetDatabase.CreateAsset(asset, $"{path}/{assetName}.asset");
        return asset;
    }

    public static TAsset LoadAsset<TAsset>(string path, string assetName) where TAsset : ScriptableObject {
        return AssetDatabase.LoadAssetAtPath<TAsset>($"{path}/{assetName}.asset");
    }

    public static void SaveAsset(Object asset) {
        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static void RemoveAsset(string path, string asset) {
        AssetDatabase.DeleteAsset($"{path}/{asset}.asset");
    }
    #endregion
}
