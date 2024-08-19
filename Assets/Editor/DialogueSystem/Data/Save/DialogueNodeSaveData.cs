using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueNodeSaveData {
    [SerializeField] private string _ID;
    [SerializeField] private string _name;
    [SerializeField] private string _text;
    [SerializeField] private List<DialogueChoiceSaveData> _choices;
    [SerializeField] private string _groupID;
    [SerializeField] private DialogueType _dialogueType;
    [SerializeField] private Vector2 _position;
}
