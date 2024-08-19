using System.Collections.Generic;
using UnityEngine;

public class DialogueSystemGraphSaveData : ScriptableObject {
    [SerializeField] private string _fileName;
    [SerializeField] private List<DialogueGroupSaveData> _groups;
    [SerializeField] private List<DialogueNodeSaveData> _nodes;
    [SerializeField] private List<string> _oldGroupNames;
    [SerializeField] private List<string> _oldUngroupedNodeNames;
    [SerializeField] private SerializableDictionary<string, List<string>> _oldGroupedNodeNames;

    public void Initialize(string fileName) {
        _fileName = fileName;
        _groups = new();
        _nodes = new();
    }
}
