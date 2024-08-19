using System;
using UnityEngine;

[Serializable]
public class DialogueChoiceSaveData {
    [SerializeField] private string _text;
    [SerializeField] private string _nodeID;

    public string Text => _text;

    public DialogueChoiceSaveData(string text) {
        SetText(text);
    }

    public void SetText(string text) {
        _text = text;
    }

    public void SetNode(DialogueBaseNode nextNode) {
        _nodeID = nextNode.ID;
    }

    public void ResetNode() {
        _nodeID = "";
    }
}
